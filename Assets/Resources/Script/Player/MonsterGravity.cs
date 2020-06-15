using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGravity : MonoBehaviour
{
    public bool inout;
    public float power;
    public float activetime;
    private void OnTriggerStay(Collider other)
    {
        if(other.tag == "Monster")
        {
            if (!inout)
                other.gameObject.transform.position = Vector3.Lerp(other.gameObject.transform.position, transform.position, power * Time.deltaTime);
            else
                other.gameObject.transform.Translate((other.gameObject.transform.position - transform.position).normalized * power * Time.deltaTime);
        }
    }
    private void Start()
    {
        Destroy(gameObject, activetime);
    }

}
