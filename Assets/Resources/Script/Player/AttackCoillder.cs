﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCoillder : MonoBehaviour
{
    private float Damage = 20.0f;

    //private void OnTriggerStay(Collider other)
    //{
        
    //}

    private void OnTriggerEnter(Collider other)
    {
        Debug.Log(1);
        if (other.gameObject.tag == "Monster")
        {
            other.gameObject.GetComponent<Gurgugi>().TakeDamage(Damage);
        }
    }



}
