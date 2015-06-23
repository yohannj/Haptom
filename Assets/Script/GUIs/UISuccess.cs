using UnityEngine;
using System.Collections;

public class UISuccess : MonoBehaviour {

    public GameObject panelSuccess;
    public GameObject panelFail;
    public CanvasGroup _canvasGroupSuccess;
    public CanvasGroup _canvasGroupFail;

	// Use this for initialization
	void Start () {
        panelSuccess = GameObject.FindGameObjectWithTag("PanelSuccess");
        panelSuccess.gameObject.SetActive(false);
        panelFail = GameObject.FindGameObjectWithTag("panelFail");
        panelFail.gameObject.SetActive(false);

        _canvasGroupSuccess.alpha = 0;
        _canvasGroupFail.alpha = 0;
	}

    public void ValiderMolecule()
    {
        bool result = AtomManager.instance.isVictoryConditionValid();

        if (result)
        {
            
            
            panelSuccess.gameObject.SetActive(true);
            _canvasGroupSuccess.alpha = 1;
        }
        else if (!result)
        {
            panelFail.gameObject.SetActive(true);
            _canvasGroupFail.alpha = 1;
        }
        else
        {
            Debug.Log("Error");
        }
    }
}
