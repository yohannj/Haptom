using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class CanvasManager : MonoBehaviour {

    public Text TimeLabel, TimeValue, HintLabel, HintValue;
    public bool isPaused = false;
    private float timer;
    

	// Use this for initialization
	void Start () {
        timer = 0f;
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
}
