using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterGravity : MonoBehaviour
{
    public bool inout;
    public float power;
    public float activetime;
    private Player player;
    private Gurgugi gurgugi;
   


    private void Start()
    {
        player = GameObject.Find("Player").GetComponent<Player>();
        gurgugi = GameObject.Find("Gurgugi_h(Clone)").GetComponent<Gurgugi>();
        Destroy(gameObject, activetime);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Monster")
        {
            if (!inout)
                other.gameObject.transform.position = Vector3.Lerp(other.gameObject.transform.position, transform.position, power * Time.deltaTime);
            else
                other.gameObject.transform.Translate((other.gameObject.transform.position - transform.position).normalized * power * Time.deltaTime);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
            gurgugi.NowHp -= 20;
    }

}
