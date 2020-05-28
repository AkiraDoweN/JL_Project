using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class popUpView : MonoBehaviour
{
    public GameObject optionWindow;

    public void gameSettings()
    {
        optionWindow.SetActive(true);
    }
    public void SettingClose()
    {
        optionWindow.SetActive(false);
    }

    public void quitGame()
    {
        Application.Quit();
    }

   


}
