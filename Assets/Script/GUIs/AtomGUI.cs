﻿using UnityEngine;
using System.Collections;
using System;

public class AtomGUI : MonoBehaviour
{
    bool is_cursor_over;
    bool atom_already_created;
    Renderer rend;

    void Start()
    {
        rend = transform.parent.GetComponent<Renderer>();
        is_cursor_over = false;
        atom_already_created = false;
    }

    void Update()
    {
        if (is_cursor_over && !atom_already_created)
        {
            bool[] buttons;
            FalconUnity.getFalconButtonStates(0, out buttons);

            if (buttons[0])
            {
                bool atom_created = GameObject.Find("Falcon").GetComponent<FalconManipulator>().tryPick();

                if (atom_created)
                {
                    atom_already_created = true;
                    var atom_name = transform.parent.name;

                    setHalo(false);
                    rend.enabled = false;

                    switch (atom_name)
                    {
                        case "Calcium":
                            AtomManager.instance.SpawnCalcium();
                            break;
                        case "Carbon":
                            AtomManager.instance.SpawnCarbon();
                            break;
                        case "Hydrogen":
                            AtomManager.instance.SpawnHydrogen();
                            break;
                        case "Nitrogen":
                            AtomManager.instance.SpawnNitrogen();
                            break;
                        case "Oxygen":
                            AtomManager.instance.SpawnOxygen();
                            break;
                        default:
                            throw new Exception("Atom name not recognized in switch");
                    }
                }
            }
        }
    }

    void setHalo(bool value)
    {
        ((Behaviour)transform.parent.GetComponent("Halo")).enabled = value;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cursor")
        {
            is_cursor_over = true;
            setHalo(true);
            AudioManager.instance.playMouseOverFalseGUI();
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.tag == "Cursor")
        {
            is_cursor_over = false;
            atom_already_created = false;
            setHalo(false);

            rend.enabled = true;
        }
    }
}