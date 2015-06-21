using UnityEngine;
using System.Collections;

public class AtomCollider : MonoBehaviour {

    bool is_cursor_over;
    int state = 1;

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
        //picked_button_pushed = true;
    }
	
	void Update () {
        if (is_cursor_over)
        {
            bool[] buttons;
            FalconUnity.getFalconButtonStates(0, out buttons);
            switch (state)
            {
                case 0:
                    if (buttons[0]) //Push button and create an atom
                    {
                        bool pick_done = GameObject.Find("Falcon").GetComponent<FalconManipulator>().tryPick();
                        if (pick_done)
                        {
                            transform.parent.GetComponent<Atom>().pick();
                            state = 1;
                        }
                    }
                    break;
                case 1:
                    if (!buttons[0]) state = 2; //Release button
                    break;
                case 2:
                    if (buttons[0]) state = 3; //Push button: will of releasing the atom
                    break;
                case 3:
                    if (!buttons[0])
                    {
                        bool release_done = transform.parent.GetComponent<Atom>().release();
                        if (release_done)
                        {
                            GameObject.Find("Falcon").GetComponent<FalconManipulator>().release();
                            state = 0;
                        }
                        else
                        {
                            state = 2;
                        }
                    }
                    break;
            }
            
            /*if (buttons[0] && !picked_button_pushed)
            {
                picked_button_pushed = true;

                bool pick_done = GameObject.Find("Falcon").GetComponent<FalconManipulator>().tryPick();
                if (pick_done) transform.parent.GetComponent<Atom>().pick();
            }
            else if (!buttons[0] && picked_button_pushed)
            {
                picked_button_pushed = false;

                bool release_done = transform.parent.GetComponent<Atom>().release();
                if (release_done)
                {
                    GameObject.Find("Falcon").GetComponent<FalconManipulator>().release();
                }
            }*/

            if (buttons[2])
            {
                state = 0;
                //TODO release if needed
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
