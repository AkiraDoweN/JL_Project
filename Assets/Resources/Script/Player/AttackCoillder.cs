using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCoillder : MonoBehaviour
{
    public PlayerCamera cameraShaking;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            StartCoroutine(cameraShaking.Shake(0.1f, 1.2f));
            //StartCoroutine(cameraShaking.Shake(0.1f, 0.5f));
        }
    }



}
