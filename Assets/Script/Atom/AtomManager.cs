using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class AtomManager : MonoBehaviour
{

    private static AtomManager _instance;

    public static AtomManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AtomManager>();
            }
            return _instance;
        }
    }

    IDictionary<GameObject, int> group_of_atom;
    IDictionary<int, HashSet<GameObject>> atoms_of_group;
    IDictionary<int, float> completion_of_group;
    IDictionary<int, Vector3> center_of_group;

    List<string> atoms_needed;

    // Use this for initialization
    void Awake()
    {
        group_of_atom = new Dictionary<GameObject, int>();
        atoms_of_group = new Dictionary<int, HashSet<GameObject>>();

        try
        {
            atoms_needed = GameProperties.instance.getMoleculeAtoms();
        }
        catch
        {
            atoms_needed = new List<String> { "Hydrogen", "Hydrogen", "Oxygen" };
        }


        atoms_needed.Sort();
    }


    public void UpdateAtomGroup()
    {
        atoms_of_group = new Dictionary<int, HashSet<GameObject>>();

        int index = 0;

        HashSet<GameObject> to_update = new HashSet<GameObject>(group_of_atom.Keys);
        List<GameObject> atom_iterator = new List<GameObject>(group_of_atom.Keys);

        foreach (GameObject atom in atom_iterator)
        {
            if (to_update.Contains(atom))
            {
                atoms_of_group.Add(index, new HashSet<GameObject>());

                Queue<GameObject> to_add = new Queue<GameObject>();
                to_add.Enqueue(atom);
                to_update.Remove(atom);

                while (to_add.Count > 0)
                {
                    GameObject tmp_atom = to_add.Dequeue();
                    group_of_atom[tmp_atom] = index;

                    atoms_of_group[index].Add(tmp_atom);

                    foreach (GameObject neighbour in tmp_atom.GetComponent<AtomCollider>().getNeighbours())
                    {
                        if (to_update.Contains(neighbour))
                        {
                            to_add.Enqueue(neighbour);
                            to_update.Remove(neighbour);
                        }
                    }
                }

                ++index;
            }
        }

        //DEBUG PURPOSE
        /*Debug.Log("#######################");
        for (int i = 0; i < atoms_of_group.Count; ++i)
        {
            String msg = "Group " + i + " contains:";
            foreach (GameObject atom in atoms_of_group[i])
            {
                msg += " " + atom.transform.parent.name;
            }

            Debug.Log(msg);
        }*/
    }

    public void UpdateAtomGroupWithPicked(GameObject go)
    {
        UpdateAtomGroup();
        atoms_of_group[group_of_atom[go]].Remove(go);
        if (atoms_of_group[group_of_atom[go]].Count == 0)
        {
            atoms_of_group.Remove(group_of_atom[go]);
        }
    }

    public Vector3 getForce(Vector3 position)
    {
        Vector3 res = new Vector3(0, 0);


        List<int> keys = new List<int>(atoms_of_group.Keys);
        if (keys.Count > 0)
        {
            foreach (int i in keys)
            {
                float delta_x = center_of_group[i].x - position.x;
                float delta_y = center_of_group[i].y - position.y;
                float delta_norm = (float) Math.Sqrt(delta_x * delta_x + delta_y * delta_y);

                if (delta_norm < 0.05) return new Vector3(0, 0);

                res.x += completion_of_group[i] * delta_x / delta_norm;
                res.y += completion_of_group[i] * delta_y / delta_norm;
            }

            res *= 3 / keys.Count;
        }


        return res;
    }

    public bool isVictoryConditionValid()
    {
        for (int i = 0; i < atoms_of_group.Count; ++i)
        {
            if (completionOf(atoms_of_group[i]) == 1f) return true;
        }

        return false;
    }

    public void computeGroupCentersAndCompletions(GameObject picked_go)
    {
        int nb_group = atoms_of_group.Count;


        completion_of_group = new Dictionary<int, float>(nb_group);
        center_of_group = new Dictionary<int, Vector3>(nb_group);
        List<int> keys = new List<int>(atoms_of_group.Keys);
        foreach (int i in keys)
        {
            //Completion part
            atoms_of_group[i].Add(picked_go);
            completion_of_group[i] = completionOf(atoms_of_group[i]);
            atoms_of_group[i].Remove(picked_go);

            //Center part
            Vector3 center = new Vector3(0, 0);
            foreach (GameObject go in atoms_of_group[i])
            {
                center.x += go.transform.position.x;
                center.y += go.transform.position.y;
            }
            center.x /= atoms_of_group[i].Count;
            center.y /= atoms_of_group[i].Count;

            center_of_group[i] = center;
        }

        /*foreach (int i in completion_of_group.Keys)
        {
            Debug.Log("Completion of group " + i + ": " + completion_of_group[i]);
        }*/
    }

    private float completionOf(HashSet<GameObject> atoms)
    {
        if (atoms.Count > atoms_needed.Count) return -1;

        int nb_ok = 0;

        List<string> atoms_names = new List<string>();
        foreach (GameObject atom in atoms)
        {
            atoms_names.Add(atom.GetComponent<AtomCollider>().getName());
        }

        atoms_names.Sort();

        int atoms_needed_index = 0;
        for (int i = 0; i < atoms_names.Count; ++i)
        {
            if (atoms_needed_index >= atoms_needed.Count) return -1f;

            int diff = atoms_names[i].CompareTo(atoms_needed[atoms_needed_index]);
            while (diff > 0)
            {
                ++atoms_needed_index;
                if (atoms_needed_index >= atoms_needed.Count) return -1f;

                diff = atoms_names[i].CompareTo(atoms_needed[atoms_needed_index]);
            }

            if (diff == 0)
            {
                ++atoms_needed_index;
                ++nb_ok;
            }
            else
            {
                return -1f;
            }
        }
        return (nb_ok * 1f) / atoms_needed.Count;
    }

    private void SpawnAtom(string name, float scale, float mass, Material material, int valence)
    {
        var new_atom_obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        new_atom_obj.transform.parent = transform;
        var new_atom = new_atom_obj.AddComponent<Atom>();
        new_atom.Set(name, scale, mass, material, valence);

        var child_go = new_atom_obj.transform.GetChild(0).gameObject;
        group_of_atom.Add(child_go, -1);
        computeGroupCentersAndCompletions(child_go);
    }

    public void SpawnCalcium()
    {
        SpawnAtom("Calcium", 0.366f, 40.078f, Resources.Load("Materials/Calcium", typeof(Material)) as Material, 2);
    }

    public void SpawnCarbon()
    {
        SpawnAtom("Carbon", 0.252f, 12.011f, Resources.Load("Materials/Carbon", typeof(Material)) as Material, 4);
    }

    public void SpawnHydrogen()
    {
        SpawnAtom("Hydrogen", 0.2f, 1.008f, Resources.Load("Materials/Hydrogen", typeof(Material)) as Material, 1);
    }

    public void SpawnNitrogen()
    {
        SpawnAtom("Nitrogen", 0.212f, 14.007f, Resources.Load("Materials/Nitrogen", typeof(Material)) as Material, 3);
    }

    public void SpawnOxygen()
    {
        SpawnAtom("Oxygen", 0.18f, 15.999f, Resources.Load("Materials/Oxygen", typeof(Material)) as Material, 2);
    }

    public void DestroyedAtom(GameObject go)
    {
        group_of_atom.Remove(go);
    }

}
