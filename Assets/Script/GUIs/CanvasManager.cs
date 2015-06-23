﻿using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasManager : MonoBehaviour {

    public Text TimeLabel, TimeValue, HintLabel, HintValue, TimeValueSuccess, HintValueSuccess,ScoreValueSucess;
    public bool isPaused = false;
    private float timer;

    public GameObject panelSuccess;
    public GameObject panelFail;
    public CanvasGroup _canvasGroupSuccess;
    public CanvasGroup _canvasGroupFail;
	private GameObject canvas;

	// Use this for initialization
	void Start () {
        timer = 0f;

        panelSuccess = GameObject.FindGameObjectWithTag("PanelSuccess");
        panelSuccess.gameObject.SetActive(false);
        panelFail = GameObject.FindGameObjectWithTag("PanelFail");
        panelFail.gameObject.SetActive(false);
		canvas = GameObject.FindGameObjectWithTag("Canvas");

        _canvasGroupSuccess.alpha = 0;
        _canvasGroupFail.alpha = 0;
	}
	
	// Update is called once per frame
	void Update () {

        if (!isPaused)
        {
            timer += Time.deltaTime;

            string minutes = Mathf.Floor(timer / 60).ToString("00");
            string seconds = Mathf.Floor(timer % 60).ToString("00");
            TimeValue.text = minutes + ":" + seconds;
        }
	}

    public void ValiderMolecule()
    {
        bool result = AtomManager.instance.isVictoryConditionValid();
        Debug.Log(result);

        if (result)
        {

            ShowSuccess();
        }
        else if (!result)
        {
            ShowFail();

        }
        else
        {
            Debug.Log("Error");
        }
        
    }

    void ShowSuccess()
    {
        GetComponent<UIPauseMenu>().setIsPausedBool(true);
        TimeValueSuccess.text = TimeValue.text;
		Debug.Log (canvas.GetComponent<Score> ().score);
		ScoreValueSucess.text = (canvas.GetComponent<Score> ().score).ToString();

        bool hintUsedValue = GetComponent<HintManager>().getUsedHint();

        if (hintUsedValue)
        {
            HintValueSuccess.text = "Used";
        }
        else
        {
            HintValueSuccess.text = "Unused";
        }

        panelSuccess.gameObject.SetActive(true);
        _canvasGroupSuccess.alpha = 1;
    }

    void ShowFail()
    {

        panelFail.gameObject.SetActive(true);
        _canvasGroupFail.alpha = 1;

        Invoke("HideFail", 3);

        
    }

    void HideFail()
    {
        panelFail.gameObject.SetActive(false);
        _canvasGroupFail.alpha = 0;
    }
}
