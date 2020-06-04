using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public enum TYPE
{
    RAIN,
    CLOUD,
    WIND
}
public class Attack : MonoBehaviour
{
    private Transform weapon;

    public int Damage = 20;
    [SerializeField]
    private TYPE type;
    //속성에 따른 빛
    [SerializeField]
    private Light playerLight;
    [SerializeField]
    private Light playerLight2;
    public GameObject coll;
    float attackTimer = 0;
    float EffectTimer = 0;

    public GameObject effectLight_b;
    public GameObject effectLight_y;
    public GameObject effectLight_g;

    public GameObject Swordeffect;


    void Start()
    {
        weapon = GetComponent<Transform>();
        type = TYPE.RAIN;
    }

    void Update()
    {
        MonsterCheck();
        WeaponChange();
        WeaponEffect();
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
                playerLight.color = new Color(0.0f, 0.28f, 1f, 1);
                //playerLight2.color = new Color(0.0f, 0.28f, 1f, 1);
                effectLight_b.SetActive(true);
                effectLight_g.SetActive(false);
                effectLight_y.SetActive(false);
                break;
            case TYPE.CLOUD:
                playerLight.color = new Color(1f, 0.92f, 0.08f, 1);
                //playerLight2.color = new Color(1f, 0.92f, 0.08f, 1);
                effectLight_b.SetActive(false);
                effectLight_g.SetActive(false);
                effectLight_y.SetActive(true);
                break;
            case TYPE.WIND:
                playerLight.color = new Color(0.20f, 0.92f, 0.09f, 1);
                //playerLight2.color = new Color(0.20f, 0.92f, 0.09f, 1);
                effectLight_b.SetActive(false);
                effectLight_g.SetActive(true);
                effectLight_y.SetActive(false);
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

    void WeaponEffect()
    {
        if (EffectTimer == 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                EffectTimer = Time.time;

            }
        }
        else
        {
            if (Time.time - EffectTimer > 0.05f)
            {
                Swordeffect.SetActive(true);
            }
            if (Time.time - EffectTimer > 0.77f)
            {
                Swordeffect.SetActive(false);
                EffectTimer = 0;
            }
        }

    }

}
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


//void PlayerAttack(Collider colider)
//{
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
//}