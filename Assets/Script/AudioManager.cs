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
	public AudioClip winlevel;
	public AudioClip loselevel;
	public AudioClip clicmenu;

    public void playMouseOverFalseGUI()
    {
        GetComponent<AudioSource>().PlayOneShot(mouseOverFalseGUI);
    }

	public void playWinLevel()
	{
		GetComponent<AudioSource>().PlayOneShot(winlevel);
	}

	public void playLoseLevel()
	{
		GetComponent<AudioSource>().PlayOneShot(loselevel);
	}
	public void playClicMenu()
	{
		GetComponent<AudioSource>().PlayOneShot(clicmenu);
	}

}