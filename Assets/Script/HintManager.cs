using UnityEngine;
using System.Collections;

public class HintManager : MonoBehaviour {

    public Hint CurrentHint;

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
    }

    public void ShowHint(Hint hint)
    {
        if (CurrentHint != null)
            CurrentHint.IsOpen = false;

            CurrentHint = hint;
            CurrentHint.IsOpen = true;

    }
}
