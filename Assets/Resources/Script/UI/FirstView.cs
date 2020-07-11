using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FirstView : MonoBehaviour
{
    float TimeCheck;
    public GameObject startView;
    // Start is called before the first frame update
    private void Start()
    {
        TimeCheck = Time.time;
    }

    // Update is called once per frame
    void Update()
    {
        TimeView();
    }

    public void TimeView()
    {
        if (Time.time - TimeCheck >= 4)
        {
            startView.SetActive(false);
        }
    }
}
