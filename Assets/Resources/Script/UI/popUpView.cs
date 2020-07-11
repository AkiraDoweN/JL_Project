using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpView : MonoBehaviour
{
    public GameObject optionWindow;

    bool IsOptionWindow;

    
    public void gameSettings()
    {
        optionWindow.SetActive(true);
        IsOptionWindow = true;
    }
    public void SettingClose()
    {
        IsOptionWindow = false;
        optionWindow.SetActive(false);
    }

    private void Update()
    {
        if(IsOptionWindow == true)
        {
            if(Input.GetKeyDown(KeyCode.Escape)){
                optionWindow.SetActive(false);
                IsOptionWindow = false;
            }
        }
    }
    
    public void quitGame()
    {
        Application.Quit();
    }

   


}
