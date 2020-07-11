﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceChange : MonoBehaviour
{
    public void ChangeMain()
    {
        SceneManager.LoadScene("Main");
        Time.timeScale = 1;

    }
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("GameScene");
        Time.timeScale = 1;

    }

    public void OpeningStart()
    {
        SceneManager.LoadScene("Opening");
    }
}
