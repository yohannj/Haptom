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
	private AudioClip Toplay;

    public void playMouseOverFalseGUI()
    {
        GetComponent<AudioSource>().PlayOneShot(mouseOverFalseGUI);
    }

	public void playWinLevel()
	{
		GetComponent<AudioSource> ().clip = winlevel;
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}

	public void playLoseLevel()
	{
		GetComponent<AudioSource> ().clip = loselevel;
		if(!GetComponent<AudioSource>().isPlaying)
			GetComponent<AudioSource>().Play();
	}
	public void playClicMenu()
	{
		GetComponent<AudioSource>().PlayOneShot(clicmenu);
	}
}