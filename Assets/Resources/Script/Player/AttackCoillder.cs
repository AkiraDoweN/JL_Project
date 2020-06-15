using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackCoillder : MonoBehaviour
{
    private SphereCollider sphereCollider;
    private float Damage = 1.0f;
    private Transform coll;
    void Start()
    {
        coll = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        Collider[] collisions = Physics.OverlapSphere(coll.position, 3f);
        foreach (Collider collider in collisions)
        {
            if (collider.gameObject.tag == "Monster")
            {
                Kill_Monster(collider);
                break;
            }
        }
    }

    public void Kill_Monster(Collider collider)
    {
        collider.gameObject.GetComponent<Gurgugi>().TakeDamage(Damage);
        
    }
}
