using UnityEngine;
using System.Collections;

public class UILevelPage : MonoBehaviour {

	//Return to MainMenu
	public void Retun () {
        Application.LoadLevel("MainMenu");
	}
	
	// Update is called once per frame
	public void LoadLevel () {
        Application.LoadLevel("main");
	}
}
