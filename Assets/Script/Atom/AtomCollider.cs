using UnityEngine;
using System.Collections;

public class AtomCollider : MonoBehaviour {

    bool is_cursor_over;

    void Awake()
    {
        GetComponent<Renderer>().enabled = false;
        transform.Rotate(90, 0, 0);

        GetComponent<Collider>().isTrigger = true;
        GetComponent<CapsuleCollider>().height = 40;
        

        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;

        is_cursor_over = true;
    }
	
	void Update () {
        if (is_cursor_over)
        {
            bool[] buttons;
            FalconUnity.getFalconButtonStates(0, out buttons);

            if (buttons[2])
            {
                Destroy(transform.parent.gameObject);
            }
        }
	}


    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cursor")
        {
            is_cursor_over = true;
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Cursor")
        {
            is_cursor_over = false;
        }
    }
}
