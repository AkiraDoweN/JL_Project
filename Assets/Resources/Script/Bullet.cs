using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletposition;
    Image image_rain;
    GameObject skillGauge_rain;

    void Start()
    {
        this.skillGauge_rain = GameObject.Find("rainSkill");
        image_rain = skillGauge_rain.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        Skill_1();
    }

    public void Skill_1()
    {
        if (skillGauge_rain.GetComponent<Image>().fillAmount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                GameObject bulletObject = Instantiate(bullet);
                bulletObject.transform.position = transform.position + transform.forward;
                bulletObject.transform.forward = bulletposition.transform.forward;
            }
        }
    }

    

}
