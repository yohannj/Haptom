using UnityEngine;
using System.Collections;

public class Atom : MonoBehaviour
{

    int valence;
    Renderer rend;

    bool is_picked = false;
    Vector3 my_init_picked_pos;
    Vector3 falcon_init_picked_pos;

    void Awake()
    {
        rend = GetComponent<Renderer>();
        var cursor_position = GameObject.FindGameObjectWithTag("Cursor").transform.position;
        transform.position = new Vector3(cursor_position.x, cursor_position.y, 2.5f);

        pick();

        var own_collider = GameObject.CreatePrimitive(PrimitiveType.Cylinder);
        own_collider.transform.parent = transform;
        own_collider.transform.position = transform.position;
        own_collider.AddComponent<AtomCollider>();
        own_collider.name = "Collider";
    }

    void Update()
    {
        if (is_picked)
        {
            Vector3 diff_pos_falcon = GameObject.FindGameObjectWithTag("Cursor").transform.position - falcon_init_picked_pos;
            transform.position = my_init_picked_pos + diff_pos_falcon;
        }
    }

    public void Set(string name, float scale, Material material, int valence)
    {
        this.transform.name = name;
        this.transform.localScale = new Vector3(scale, scale, scale);
        this.rend.material = material;
        this.valence = valence;
    }

    protected internal void pick()
    {
        is_picked = true;

        GameObject.FindGameObjectWithTag("CursorRenderer").GetComponent<Renderer>().enabled = false;
        my_init_picked_pos = transform.position;
        falcon_init_picked_pos = GameObject.FindGameObjectWithTag("Cursor").transform.position;
    }

    protected internal bool release()
    {
        if (transform.FindChild("Collider").GetComponent<AtomCollider>().getOverAtomGUICounter() == 0)
        {
            is_picked = false;
            GameObject.FindGameObjectWithTag("CursorRenderer").GetComponent<Renderer>().enabled = true;
            return true;
        }
        else
        {
            return false;
        }
    }
}
