using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterAttack : MonoBehaviour
{

    [SerializeField]
    private int Damage = 20;
    private Player player;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Attack();
    }


    void Attack()
    {
        if (player.NowHp > 0)
            player.TakeDamage(Damage);
    }
}
