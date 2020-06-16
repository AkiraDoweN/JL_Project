using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpView : MonoBehaviour
{
    public GameObject optionWindow;
    public GameObject startView;

    public void gameSettings()
    {
        optionWindow.SetActive(true);
    }
    public void SettingClose()
    {
        optionWindow.SetActive(false);
    }

    private void Update()
    {
        TimeView();
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
