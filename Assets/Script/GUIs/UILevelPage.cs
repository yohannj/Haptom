using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class UILevelPage : MonoBehaviour {

    public GameObject panelTab1;
    public GameObject panelTab2;
    public CanvasGroup _canvasGroupTab1;
    public CanvasGroup _canvasGroupTab2;

    void Start()
    {
        List<int> listLevelSucceded = GameProperties.instance.getListSuccess();

        if (listLevelSucceded.Count == 0)
        {
            panelTab2 = GameObject.FindGameObjectWithTag("Tab2");
            panelTab2.gameObject.SetActive(false);
            _canvasGroupTab2.alpha = 0;
        }
        else
        {
            panelTab1 = GameObject.FindGameObjectWithTag("Tab1");
            panelTab1.gameObject.SetActive(false);
            _canvasGroupTab1.alpha = 0;
        }
    }
	//Return to MainMenu
	public void Retun () {
        Application.LoadLevel("MainMenu");
	}
	
	// Load level
	public void LoadLevel (int level) {
        GameProperties.instance.setLevel(level);
        Application.LoadLevel("main");
		print ("Chargement du lvl 1");
	}
}
