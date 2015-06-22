using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AtomCollider : MonoBehaviour
{

    HashSet<GameObject> neighbours;

    bool is_cursor_over;
    int state = 1;

    int over_AtomGUI_counter = 0;

    void Awake()
    {
        neighbours = new HashSet<GameObject>();
        GetComponent<Renderer>().enabled = false;
        transform.Rotate(90, 0, 0);

        GetComponent<Collider>().isTrigger = true;
        GetComponent<CapsuleCollider>().height = 40;


        gameObject.AddComponent<Rigidbody>();
        GetComponent<Rigidbody>().useGravity = false;

        is_cursor_over = true;
    }

    void Update()
    {
        if (is_cursor_over)
        {
            bool[] buttons;
            FalconUnity.getFalconButtonStates(0, out buttons);

            bool pushing = buttons[0];
            bool deleting = buttons[2];

            switch (state)
            {
                case 0:
                    if (pushing) //Push button and create an atom
                    {
                        bool pick_done = GameObject.Find("Falcon").GetComponent<FalconManipulator>().tryPick();
                        if (pick_done)
                        {
                            transform.parent.GetComponent<Atom>().pick();
                            state = 1;
                            AtomManager.instance.UpdateAtomGroupWithPicked(gameObject);
                        }
                    }
                    break;
                case 1:
                    if (!pushing) state = 2; //Release button
                    break;
                case 2:
                    if (pushing) state = 3; //Push button: will of releasing the atom
                    break;
                case 3:
                    if (!pushing) //Try to release the atom
                    {
                        bool release_done = transform.parent.GetComponent<Atom>().release();
                        if (release_done)
                        {
                            GameObject.Find("Falcon").GetComponent<FalconManipulator>().release();
                            state = 0;
                            AtomManager.instance.UpdateAtomGroup();
                        }
                        else
                        {
                            state = 2;
                        }
                    }
                    break;
            }

            if (deleting && !pushing)
            {
                if (state != 0)
                {
                    GameObject.Find("Falcon").GetComponent<FalconManipulator>().release();
                    GameObject.FindGameObjectWithTag("CursorRenderer").GetComponent<Renderer>().enabled = true;
                }
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
        else if (other.GetComponent<AtomGUI>() != null)
        {
            ++over_AtomGUI_counter;
        }
        else if (other.GetComponent<AtomCollider>() != null)
        {
            neighbours.Add(other.gameObject);
        }
    }

    void OnTriggerExit(Collider other)
    {

        if (other.tag == "Cursor")
        {
            is_cursor_over = false;
        }
        else if (other.GetComponent<AtomGUI>() != null)
        {
            --over_AtomGUI_counter;
        }
        else if (other.GetComponent<AtomCollider>() != null)
        {
            neighbours.Remove(other.gameObject);
        }
    }

    public string getName()
    {
        return transform.parent.name;
    }

    public int getOverAtomGUICounter()
    {
        return over_AtomGUI_counter;
    }

    public HashSet<GameObject> getNeighbours()
    {
        return new HashSet<GameObject>(neighbours);
    }

    public bool isValenceOK() {
        int valence = transform.parent.GetComponent<Atom>().getValence();

        foreach (GameObject neighbour in neighbours)
        {
            valence -= neighbour.transform.parent.GetComponent<Atom>().getValence();
        }

        if (valence <= 0)
        {
            return true;
        }

        return false;
    }
}
