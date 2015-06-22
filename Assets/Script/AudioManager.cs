using UnityEngine;
using System.Collections;

public class AudioManager : MonoBehaviour
{
    private static AudioManager _instance;

    public static AudioManager instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = GameObject.FindObjectOfType<AudioManager>();
            }
            return _instance;
        }
    }

    public AudioClip mouseOverFalseGUI;

    public void playMouseOverFalseGUI()
    {
        GetComponent<AudioSource>().PlayOneShot(mouseOverFalseGUI);
    }
}