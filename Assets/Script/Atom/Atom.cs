using UnityEngine;
using System.Collections;

public class Atom : MonoBehaviour {

    string name;
    int valence;
    Renderer rend;

    bool is_picked;

	// Use this for initialization
	void Start () {
        rend = GetComponent<Renderer>();
        is_picked = true;
	}
	
	// Update is called once per frame
	void Update () {
	    
	}

    void Set(string name, float scale, Material material, int valence)
    {
        this.name = name;
        this.transform.localScale = new Vector3(scale, scale, scale);
        this.rend.material = material;
        this.valence = valence;
    }
}
