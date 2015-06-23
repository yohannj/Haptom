using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class Score : MonoBehaviour {

	public Double score = 0f;
	private GameObject GO;
	private string timer;
	private bool hint;
	private int level;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

		GO = GameObject.Find("Time_value");
		timer = GO.GetComponent<Text> ().text;
		
		double time;
		double coefhint = GetHintCoef ();
		double coeflvl = GetLevelCoef ();

		//1/t(s)1000*1.lvl* coef indice

		time = 60*int.Parse(timer.Substring (0, 2)) + int.Parse(timer.Substring (3, 2));
		//Debug.Log (time);	
		if (time == 0)
			time = 1;

		score = Math.Truncate((1800 - time)  * coefhint * coeflvl);
		//Debug.Log (score);
	}

	public double GetLevelCoef(){
		double coef = 1;
		try
		{
			level = GameProperties.instance.getLevel();
		}
		catch 
		{
			level = 1;
		}
		coef += level / 10;

		return coef;	
	}

	public double GetHintCoef(){
		double coef;
		hint = GetComponent<HintManager>().getUsedHint();

		if (!hint)
			coef = 2;
		else
			coef = 1;

		return coef;	
	}
}