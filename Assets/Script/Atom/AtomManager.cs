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
                DontDestroyOnLoad(_instance.gameObject);
            }
            return _instance;
        }
    }

    IDictionary<GameObject, int> group_of_atom;
    IDictionary<int, HashSet<GameObject>> atoms_of_group;
    IDictionary<int, float> completion_of_group;

    // Use this for initialization
    void Awake()
    {
        group_of_atom = new Dictionary<GameObject, int>();
        atoms_of_group = new Dictionary<int, HashSet<GameObject>>();
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
    }

    public void computeGroupCompletions()
    {
        completion_of_group = new Dictionary<int, float>();
        foreach(int i in atoms_of_group.Keys) {

        }
    }

    public bool isVictoryConditionValid()
    {
        int level;
        try
        {
            level = GameProperties.instance.getLevel();
        }
        catch
        {
            level = 1;
        }

        List<string> atom_needed = new List<string>();
        switch (level)
        {
            case 1:
                atom_needed = new List<String> { "Hydrogen", "Hydrogen", "Oxygen" };
                break;
        }

        atom_needed.Sort();

        bool success;
        for (int i = 0; i < atoms_of_group.Count; ++i)
        {

            if (atoms_of_group[i].Count == atom_needed.Count)
            {
                success = true;
                List<string> atoms_names = new List<string>();

                foreach (GameObject atom in atoms_of_group[i])
                {
                    success &= atom.GetComponent<AtomCollider>().isValenceOK();
                    atoms_names.Add(atom.GetComponent<AtomCollider>().getName());
                }

                atoms_names.Sort();

                for (int j = 0; j < atom_needed.Count; ++j)
                {
                    success &= atom_needed[j] == atoms_names[j];
                }

                    if (success) return true;
            }
        }

        return false;
    }

    private GameObject SpawnAtom(string name, float scale, float mass, Material material, int valence)
    {
        var new_atom_obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        new_atom_obj.transform.parent = transform;
        var new_atom = new_atom_obj.AddComponent<Atom>();
        new_atom.Set(name, scale, mass, material, valence);

        group_of_atom.Add(new_atom_obj.transform.GetChild(0).gameObject, -1);

        return new_atom_obj;
    }

    public GameObject SpawnCalcium()
    {
        return SpawnAtom("Calcium", 0.366f, 40.078f, Resources.Load("Materials/Calcium", typeof(Material)) as Material, 2);
    }

    public GameObject SpawnCarbon()
    {
        return SpawnAtom("Carbon", 0.252f, 12.011f, Resources.Load("Materials/Carbon", typeof(Material)) as Material, 4);
    }

    public GameObject SpawnHydrogen()
    {
        return SpawnAtom("Hydrogen", 0.2f, 1.008f, Resources.Load("Materials/Hydrogen", typeof(Material)) as Material, 1);
    }

    public GameObject SpawnNitrogen()
    {
        return SpawnAtom("Nitrogen", 0.212f, 14.007f, Resources.Load("Materials/Nitrogen", typeof(Material)) as Material, 3);
    }

    public GameObject SpawnOxygen()
    {
        return SpawnAtom("Oxygen", 0.18f, 15.999f, Resources.Load("Materials/Oxygen", typeof(Material)) as Material, 2);
    }

    public void DestroyedAtom(GameObject go)
    {
        group_of_atom.Remove(go);
    }

}
