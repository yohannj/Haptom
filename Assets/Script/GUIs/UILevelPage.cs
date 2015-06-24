using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;

public class UILevelPage : MonoBehaviour {

    public GameObject[] panelsCad;
    
    private GameObject panelHe;
    private GameObject panelLi;
    private GameObject panelBe;
    private GameObject panelB;

    private CanvasGroup _canvasGroupHe;
    private CanvasGroup _canvasGroupli;
    private CanvasGroup _canvasGroupBe;
    private CanvasGroup _canvasGroupB;

    public GameObject panelTab1;
    public GameObject panelTab2;
    
    public CanvasGroup _canvasGroupTab1;
    public CanvasGroup _canvasGroupTab2;

    void Start()
    {
        List<int> listLevelSucceded = GameProperties.instance.getListSuccess();

        panelsCad = GameObject.FindGameObjectsWithTag("PanelButton");


        panelHe = GameObject.Find("ButtonHe");
        panelLi = GameObject.Find("ButtonLi");
        panelBe = GameObject.Find("Buttonbe");
        panelB = GameObject.Find("ButtonB");

        if (listLevelSucceded.Count>5)
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

        switchViewPadLock(listLevelSucceded.Count);

       
    }
	//Return to MainMenu
	public void Retun () {
		AudioManager.instance.playClicMenu();
        Application.LoadLevel("MainMenu");
	}
	
	// Load level
	public void LoadLevel (int level) {
		AudioManager.instance.playClicMenu();
        GameProperties.instance.setLevel(level);
        Application.LoadLevel("main");
		print ("Chargement du lvl 1");
	}

    void switchViewPadLock(int listSize)
    {
        foreach (GameObject panel in panelsCad)
        {
            panel.GetComponent<Button>().enabled = false;
            panel.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1;
        }

        switch (listSize)
        {
            case 0: break;

            case 1: panelHe.GetComponent<Button>().enabled = true;
                    panelHe.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    break;

            case 2: panelHe.gameObject.SetActive(true);
                    panelHe.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    panelLi.gameObject.SetActive(true);
                    panelLi.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    break;

            case 3: panelHe.gameObject.SetActive(true);
                    panelHe.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    panelLi.gameObject.SetActive(true);
                    panelLi.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    panelBe.gameObject.SetActive(true);
                    panelBe.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    break;

            case 4: panelHe.gameObject.SetActive(true);
                    panelHe.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    panelLi.gameObject.SetActive(true);
                    panelLi.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    panelBe.gameObject.SetActive(true);
                    panelBe.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    panelB.gameObject.SetActive(true);
                    panelB.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    break;

            case 5: foreach (GameObject panel in panelsCad)
                    {
                        panel.gameObject.SetActive(true);
                        panel.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 0;
                    }
                    break;
                
                                
                    /*.gameObject.SetActive(true);
                    panelHe.transform.GetChild(0).GetComponent<CanvasGroup>().alpha = 1;
                    panelLi.gameObject.SetActive(true);
                    panelBe.gameObject.SetActive(true);
                    panelB.gameObject.SetActive(true);
                    break;*/

        }
    }

}
