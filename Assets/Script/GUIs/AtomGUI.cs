using UnityEngine;
using System.Collections;

public class AtomGUI : MonoBehaviour
{

    void OnTriggerEnter (Collider other)
    {
        ((Behaviour) transform.parent.GetComponent("Halo")).enabled = true;
    }

    void OnTriggerExit (Collider other)
    {
        ((Behaviour)transform.parent.GetComponent("Halo")).enabled = false;
    }
}