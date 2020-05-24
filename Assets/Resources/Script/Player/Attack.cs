using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    private Transform weapon;
    private Gurgugi gurgugi;

    public int Damage = 20;

    //속성
    bool rainProperty = true;
    bool cloudProperty = true;
    bool windProperty = true;

    //속성에 따른 빛
    public Light rainLight;
    public Light cloudLight;
    public Light windLight;

    void Start()
    {
        weapon = GetComponent<Transform>();
        gurgugi = GetComponent<Gurgugi>();

        rainLight.enabled = true;
        cloudLight.enabled = false;
        windLight.enabled = false;
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

        if (Input.GetKey(KeyCode.Q)) //Rain
        {
            rainLight.enabled = true;
            cloudLight.enabled = false;
            windLight.enabled = false;

            rainProperty = true;
            cloudProperty = false;
            windProperty = false;
        }
        else if (Input.GetKey(KeyCode.W)) //Cloud
        {
            rainLight.enabled = false;
            cloudLight.enabled = true;
            windLight.enabled = false;

            rainProperty = false;
            cloudProperty = true;
            windProperty = false;
        }
        else if (Input.GetKey(KeyCode.E)) //Wind
        {
            rainLight.enabled = false;
            cloudLight.enabled = false;
            windLight.enabled = true;

            rainProperty = false;
            cloudProperty = false;
            windProperty = true;
        }
        else
        {
            Damage = 20;
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
