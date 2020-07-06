using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCoillder : MonoBehaviour
{
    public PlayerCamera cameraShaking;
    public float damageTimeout = 1f;
    private bool canTakeDamage = true;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            StartCoroutine(cameraShaking.Shake(0.1f, 1.2f));
            //StartCoroutine(damageTimer());

        }
    }

    private IEnumerator damageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;
    }

}
