using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System.Collections.Generic;
public class Pause : MonoBehaviour
{
    public GameObject optionWindow;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0;
            optionWindow.SetActive(true);
        }
    }

    public void PauseOut()
    {
        Time.timeScale = 1;
        optionWindow.SetActive(false);
    }
}
