using UnityEngine;
using System.Collections;

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

    private int index = -1;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    private GameObject SpawnAtom(string name, float scale, Material material, int valence)
    {
        var new_atom_obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        new_atom_obj.transform.parent = transform;
        var new_atom = new_atom_obj.AddComponent<Atom>();
        new_atom.Set(name + "_" + (++index), scale, material, valence);

        return new_atom_obj;
    }

    public GameObject SpawnCalcium()
    {
        return SpawnAtom("Calcium", 0.366f, Resources.Load("Materials/Calcium", typeof(Material)) as Material, 2);
    }

    public GameObject SpawnCarbon()
    {
        return SpawnAtom("Carbon", 0.252f, Resources.Load("Materials/Carbon", typeof(Material)) as Material, 4);
    }

    public GameObject SpawnHydrogen()
    {
        return SpawnAtom("Hydrogen", 0.2f, Resources.Load("Materials/Hydrogen", typeof(Material)) as Material, 1);
    }

    public GameObject SpawnNitrogen()
    {
        return SpawnAtom("Nitrogen", 0.212f, Resources.Load("Materials/Nitrogen", typeof(Material)) as Material, 3);
    }

    public GameObject SpawnOxygen()
    {
        return SpawnAtom("Oxygen", 0.18f, Resources.Load("Materials/Oxygen", typeof(Material)) as Material, 2);
    }

}
