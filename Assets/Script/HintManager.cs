using UnityEngine;
using System.Collections;

public class HintManager : MonoBehaviour {

    public Hint CurrentHint;

    public bool hintUsed;

    void Start()
    {
        hintUsed = false;
    }

    // Use this for initialization
  /*  public void Update()
    {
         if(Input.GetKeyDown(KeyCode.H))
        {
            ShowHint(CurrentHint);
        }
    }*/

    public void clickShowHint()
    {
        ShowHint(CurrentHint);
		AudioManager.instance.playClicMenu();
        hintUsed = true;
    }

    public void ShowHint(Hint hint)
    {
        if (CurrentHint != null)
            CurrentHint.IsOpen = false;

            CurrentHint = hint;
            CurrentHint.IsOpen = true;

    }

    public bool getUsedHint()
    {
        return hintUsed;
    }
}
