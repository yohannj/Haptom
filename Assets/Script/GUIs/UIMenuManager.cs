﻿using UnityEngine;
using System.Collections;

public class UIMenuManager : MonoBehaviour {

	// Launch game
	public void StartGame () 
    {
        Application.LoadLevel("LevelPage");
	}
	
	//Quit Game
    public void QuitGame ()
    {
		print (" J'ai quitté!");
        Application.Quit();
	}
}
