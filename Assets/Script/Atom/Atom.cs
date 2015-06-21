using UnityEngine;
using System.Collections;

public class Atom : MonoBehaviour {

    int valence;
    Renderer rend;

    bool is_picked;

	void Awake () {
        rend = GetComponent<Renderer>();
        var cursor_position = GameObject.FindGameObjectWithTag("Cursor").transform.position;
        transform.position = new Vector3(cursor_position.x, cursor_position.y, 2.5f);

        is_picked = true;

        var own_collider = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        own_collider.transform.parent = transform;
        own_collider.transform.position = transform.position;
        own_collider.AddComponent<AtomCollider>();
        own_collider.name = "Collider";
	}
	
	void Update () {
        
	}

    public void Set(string name, float scale, Material material, int valence)
    {
        this.transform.name = name;
        this.transform.localScale = new Vector3(scale, scale, scale);
        this.rend.material = material;
        this.valence = valence;
    }
}
