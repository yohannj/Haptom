using UnityEngine;
using System.Collections;

public class AtomManager : MonoBehaviour {

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

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    private GameObject SpawnAtom(string name, float scale, Material material, int valence)
    {
        var new_atom_obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        var new_atom = new_atom_obj.AddComponent<Atom>();

        return new_atom_obj;
    }

    public GameObject SpawnCalcium()
    {
        return SpawnAtom("Calcium", 3.66f, Resources.Load("Materials/Calcium", typeof(Material)) as Material, 2);
    }

    public GameObject SpawnCarbon()
    {
        return SpawnAtom("Carbon", 1.26f, Resources.Load("Materials/Carbon", typeof(Material)) as Material, 4);
    }

}
