using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Pause : MonoBehaviour
{
    bool Is_popUp = true;
    public GameObject optionWindow;
    public GameObject UIOnOff;
    public GameObject UIOnOff2;
    public GameObject UIOnOff3;
    public GameObject UIOnOff4;
    public GameObject UIOnOff5;
    public GameObject UIOnOff6;
    public GameObject UIOnOff7;
    public GameObject UIOnOff8;
    public GameObject UIOnOff9;
    public GameObject UIOnOff10;

    // Update is called once per frame
    void Update()
    {
        PauseCheck();
    }

    public void PauseCheck()
    {
        if (Is_popUp == true)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 0;
                optionWindow.SetActive(true);
                UIOnOff2.SetActive(false);
                UIOnOff3.SetActive(false);
                UIOnOff4.SetActive(false);
                UIOnOff5.SetActive(false);
                UIOnOff6.SetActive(false);
                UIOnOff7.SetActive(false);
                UIOnOff8.SetActive(false);
                UIOnOff9.SetActive(false);
                UIOnOff10.SetActive(false);
                Is_popUp = false;
            }

        }
        else if (Is_popUp == false)
        {
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                Time.timeScale = 1;
                optionWindow.SetActive(false);
                UIOnOff2.SetActive(true);
                UIOnOff3.SetActive(true);
                UIOnOff4.SetActive(true);
                UIOnOff5.SetActive(true);
                UIOnOff6.SetActive(true);
                UIOnOff7.SetActive(true);
                UIOnOff8.SetActive(true);
                UIOnOff9.SetActive(true);
                UIOnOff10.SetActive(true);
                Is_popUp = true;
            }
        }
    }

    public void PauseCheck_button()
    {
        if (Is_popUp == true)
        {
                Time.timeScale = 0;
                optionWindow.SetActive(true);
                UIOnOff2.SetActive(false);
                UIOnOff3.SetActive(false);
                UIOnOff4.SetActive(false);
                UIOnOff5.SetActive(false);
                UIOnOff6.SetActive(false);
                UIOnOff7.SetActive(false);
                UIOnOff8.SetActive(false);
                UIOnOff9.SetActive(false);
                UIOnOff10.SetActive(false);
                Is_popUp = false;
        }
        else if (Is_popUp == false)
        {
                Time.timeScale = 1;
                optionWindow.SetActive(false);
                UIOnOff2.SetActive(true);
                UIOnOff3.SetActive(true);
                UIOnOff4.SetActive(true);
                UIOnOff5.SetActive(true);
                UIOnOff6.SetActive(true);
                UIOnOff7.SetActive(true);
                UIOnOff8.SetActive(true);
                UIOnOff9.SetActive(true);
                UIOnOff10.SetActive(true);
                Is_popUp = true;
        }
    }
}
