using UnityEngine;
using System.Collections;

public class UIPauseMenu : MonoBehaviour {
	
	private bool IsPaused = false;

	// Use this for initialization
	void Start () {
		Canvas canvas = GetComponent<Canvas>();
		canvas.enabled = false;
	}
	
	// Update is called once per frame
	void Update () {
		Canvas canvas = GetComponent<Canvas>();

		if(Input.GetKeyDown("p")||Input.GetKeyDown(KeyCode.Escape) && !IsPaused)
		{
			Time.timeScale = 0;
			canvas.enabled = true;
			IsPaused = true;
		}
		else if (Input.GetKeyDown("p")||Input.GetKeyDown(KeyCode.Escape)&& IsPaused)
		{
			Time.timeScale = 1;
			canvas.enabled = false;
			IsPaused = false;
		}
		
	}

	public void Continue(){
		Canvas canvas = GetComponent<Canvas>();
		print ("ispaused :" + IsPaused);
		if (IsPaused)
		{
			Time.timeScale = 1;
			canvas.enabled = false;
			IsPaused = false;
		}
	}
}
