using UnityEngine;
using System.Collections;

public class AtomCollider : MonoBehaviour {

    bool is_cursor_over;
    bool picked_button_pushed;

    int over_AtomGUI_counter = 0;

    void Awake()
    {
        GetComponent<Renderer>().enabled = false;
        transform.Rotate(90, 0, 0);

        GetComponent<Collider>().isTrigger = true;
        GetComponent<CapsuleCollider>().height = 40;
        

        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;

        is_cursor_over = true;
        picked_button_pushed = true;
    }
	
	void Update () {
        if (is_cursor_over)
        {
            bool[] buttons;
            FalconUnity.getFalconButtonStates(0, out buttons);

            if (buttons[0] && !picked_button_pushed)
            {
                picked_button_pushed = GameObject.Find("Falcon").GetComponent<FalconManipulator>().tryPick();

                if(picked_button_pushed) transform.parent.GetComponent<Atom>().pick();
            }
            else if (!buttons[0] && picked_button_pushed)
            {
                bool release_done = transform.parent.GetComponent<Atom>().release();

                if (release_done)
                {
                    picked_button_pushed = false;
                    GameObject.Find("Falcon").GetComponent<FalconManipulator>().release();
                }
            }

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
        } else if (other.GetComponent<AtomGUI>() != null)
        {
            ++over_AtomGUI_counter;
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Cursor")
        {
            is_cursor_over = false;
        } else if (other.GetComponent<AtomGUI>() != null)
        {
            --over_AtomGUI_counter;
        }
    }


    public int getOverAtomGUICounter() {
        return over_AtomGUI_counter;
    }
}
