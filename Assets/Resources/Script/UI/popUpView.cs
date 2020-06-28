using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpView : MonoBehaviour
{
    public GameObject optionWindow;
    public GameObject startView;

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
        TimeView();
        if(IsOptionWindow == true)
        {
            if(Input.GetKeyDown(KeyCode.Escape)){
                optionWindow.SetActive(false);
                IsOptionWindow = false;
            }
        }
    }
    public void TimeView()
    {
        if(Time.time > 4)
        {
            startView.SetActive(false);

        }
    }

    public void quitGame()
    {
        Application.Quit();
    }

   


}
