using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public int Damage = 100;
    public float Speed = 1000.0f;

    void Start()
    {
        GetComponent<Rigidbody>().AddForce(transform.forward * Speed);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
