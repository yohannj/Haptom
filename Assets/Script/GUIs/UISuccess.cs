using UnityEngine;
using System.Collections;

public class UISuccess : MonoBehaviour {


    public GameObject panel;
    public CanvasGroup _canvasGroup;
	// Use this for initialization
	void Start () {
        panel = GameObject.FindGameObjectWithTag("PanelSuccess");
        panel.gameObject.SetActive(false);

        _canvasGroup.alpha = 0;
	}
	
	
}
