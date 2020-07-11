using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpeningScript1 : MonoBehaviour
{
    private float _TimeCheck;
    // Start is called before the first frame update
    void Start()
    {
        _TimeCheck = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        OpeningSenceChange();
    }

    public void OpeningSenceChange()
    {
        if (Time.time - _TimeCheck >= 3)
        {
            SceneManager.LoadScene("Opening3");
        }
    }
}
