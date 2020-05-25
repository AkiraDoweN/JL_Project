using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public enum TYPE
{
    RAIN,
    CLOUD,
    WIND
}
public class Attack : MonoBehaviour
{
    private Transform weapon;

    [SerializeField]
    private int Damage = 20;
    [SerializeField]
    private TYPE type;
    //속성에 따른 빛
    [SerializeField]
    private Light playerLight;
    public GameObject coll;
    float attackTimer = 0;


    void Start()
    {
        weapon = GetComponent<Transform>();
        type = TYPE.RAIN;
    }

    void Update()
    {
        MonsterCheck();
        WeaponChange();
    }

    void WeaponChange()
    {
        if (Input.GetKey(KeyCode.Q)) //Rain
        {
            type = TYPE.RAIN;
        }
        else if (Input.GetKey(KeyCode.W)) //Cloud
        {
            type = TYPE.CLOUD;
        }
        else if (Input.GetKey(KeyCode.E)) //Wind
        {
            type = TYPE.WIND;
        }
        else
        {
            Damage = 20;
        }
        SetWeapon();
    }
    
    void SetWeapon()
    {
        switch(type)
        {
            case TYPE.RAIN:
                playerLight.color = new Color(0.13f, 0.56f, 0.99f, 1);
                break;
            case TYPE.CLOUD:
                playerLight.color = new Color(0.87f, 0.92f, 0.02f, 1);
                break;
            case TYPE.WIND:
                playerLight.color = new Color(0.76f, 1, 0.18f, 1);
                break;
        }
    }

    void MonsterCheck()
    {
        if (attackTimer == 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                attackTimer = Time.time;

                //Collider[] collisions = Physics.OverlapCapsule(weapon.position, weapon.position, 0.3f);
                //foreach (Collider collider in collisions)
                //{
                //    if (collider.gameObject.tag == "Monster")
                //    {
                //        collider.GetComponent<Gurgugi>().Dead();
                //        PlayerAttack(collider);
                //        break;
                //    }
                //}
            }
        }
        else
        {
            if (Time.time - attackTimer > 0.2f)
            {
                coll.SetActive(true);
            }
            if (Time.time - attackTimer > 0.51f)
            {
                coll.SetActive(false);
                attackTimer = 0;
            }
        }
        
    }

    void PlayerAttack(Collider colider)
    {
        // colider.gameObject.GetComponent<EnemyHealth>().TakeDamage(attackDamage);
        
        //몬스터속성 script bool값추가 (몬스터 속성과 Player 속성이 같을 경우 데미지 2배)
        //if (rainProperty == true && gurgugi.Property == true)
        //{
        //    Damage = 40;
        //}
        //else if (cloudProperty == true && gurgugi.Property == true)
        //{
        //    Damage = 40;
        //}
        //else if (windProperty == true && gurgugi.Property == true)
        //{
        //    Damage = 40;
        //}
        //else
        //{
        //    Damage = 20;
        //}
    }

}
