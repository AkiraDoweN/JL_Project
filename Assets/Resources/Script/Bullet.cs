using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletposition;

    void Start()
    {

    }
    
    // Update is called once per frame
    void Update()
    {
        Skill_1();
    }

    public void Skill_1()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject bulletObject = Instantiate(bullet);
            bulletObject.transform.position = transform.position + transform.forward;
            bulletObject.transform.forward = bulletposition.transform.forward;
        }
    }

    

}
