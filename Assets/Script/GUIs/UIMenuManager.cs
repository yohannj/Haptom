using UnityEngine;
using System.Collections;

public class UIMenuManager : MonoBehaviour {

	// Launch game
	public void StartGame () 
    {
		AudioManager.instance.playClicMenu();
        Application.LoadLevel("LevelPage");
	}
	
	//Quit Game
    public void QuitGame ()
    {
		AudioManager.instance.playClicMenu();
		print (" J'ai quitté!");
        Application.Quit();
	}
}
