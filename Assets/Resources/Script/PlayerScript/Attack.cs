using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int Damage = 20;
    private Transform weapon;

    void Start()
    {
        weapon = GetComponent<Transform>();
    }

    void Update()
    {
        Collider[] collisions = Physics.OverlapCapsule(weapon.position, weapon.position, 0.3f);
        foreach (Collider collider in collisions)
        {
            if (collider.gameObject.tag == "Monster")
            {
                PlayerAttack(collider);
                break;
            }
        }
    }
    void PlayerAttack(Collider colider)
    {
       // colider.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
    }

}
