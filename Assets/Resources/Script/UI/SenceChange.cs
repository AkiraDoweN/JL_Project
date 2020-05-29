using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SenceChange : MonoBehaviour
{
    public void ChangeMain()
    {
        SceneManager.LoadScene("Main");
    }
    public void ChangeGameScene()
    {
        SceneManager.LoadScene("SubSence");
    }
}
