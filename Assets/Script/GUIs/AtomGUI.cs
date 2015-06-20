using UnityEngine;
using System.Collections;

public class AtomGUI : MonoBehaviour
{
    bool is_cursor_over;
    bool atom_already_created;

    void Start()
    {
        is_cursor_over = false;
        atom_already_created = false;
    }

    void Update ()
    {
        if (is_cursor_over && !atom_already_created)
        {
            bool[] buttons;
            FalconUnity.getFalconButtonStates(0, out buttons);

            if (buttons[0])
            {
                atom_already_created = true;
                GameObject new_atom;
                var atom_name = transform.parent.name;


                switch (atom_name)
                {
                    case "Calcium":
                        new_atom = AtomManager.instance.SpawnCalcium();
                        //new_atom.transform = Falcon.instance
                        break;
                    case "Carbon":
                        new_atom = AtomManager.instance.SpawnCarbon();
                        break;
                    case "Hydrogen":
                        new_atom = AtomManager.instance.SpawnHydrogen();
                        break;
                    case "Nitrogen":
                        new_atom = AtomManager.instance.SpawnNitrogen();
                        break;
                    case "Oxygen":
                        new_atom = AtomManager.instance.SpawnOxygen();
                        break;
                    default:
                        break;
                }
            }

            if (buttons[2])
            {
                Destroy(transform.parent.gameObject);
            }
        }
    }

    void OnTriggerEnter (Collider other)
    {
        is_cursor_over = true;
        ((Behaviour) transform.parent.GetComponent("Halo")).enabled = true;
    }

    void OnTriggerExit (Collider other)
    {
        is_cursor_over = false;
        atom_already_created = false;
        ((Behaviour)transform.parent.GetComponent("Halo")).enabled = false;
    }
}