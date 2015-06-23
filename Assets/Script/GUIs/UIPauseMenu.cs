using UnityEngine;
using System.Collections;

public class UIPauseMenu : MonoBehaviour
{

    public bool IsPaused = false;
    public GameObject panel;
    public CanvasGroup _canvasGroup;
    //Canvas canvas;

    // Use this for initialization
    void Start()
    {
        //canvas = GetComponent<Canvas>();
        //canvas.enabled = false;

        panel = GameObject.FindGameObjectWithTag("MenuPause");
        panel.gameObject.SetActive(false);

        _canvasGroup.alpha = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if ((Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape)) && !IsPaused)
        {
            Time.timeScale = 0;
            panel.gameObject.SetActive(true);
            _canvasGroup.alpha = 1;
            //canvas.enabled = true;
            IsPaused = true;
        }
        else if ((Input.GetKeyDown("p") || Input.GetKeyDown(KeyCode.Escape)) && IsPaused)
        {
            Time.timeScale = 1;
            panel.gameObject.SetActive(false);
            _canvasGroup.alpha = 0;
            //canvas.enabled = false;
            IsPaused = false;
        }

    }

    public void Continue()
    {
		AudioManager.instance.playClicMenu();
        if (IsPaused)
        {
            Time.timeScale = 1;
            panel.gameObject.SetActive(false);
            _canvasGroup.alpha = 0;
            //canvas.enabled = false;
            IsPaused = false;
        }
    }

    public void LoadLevel()
    {
		AudioManager.instance.playClicMenu();
        print("Chargement du lvl 1");
        Application.LoadLevel("main");
        IsPaused = false;
    }

    public void LoadLevelPage()
    {
		AudioManager.instance.playClicMenu();
        Application.LoadLevel("LevelPage");
    }

    //Quit Game
    public void QuitGame()
    {
		AudioManager.instance.playClicMenu();
        print(" J'ai quitté!");
        Application.Quit();
    }

    public void Unpause()
    {
        IsPaused = false;
        Time.timeScale = 1;

    }
}
