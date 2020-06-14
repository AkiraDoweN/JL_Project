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
    
    public GameObject coll;
    float attackTimer = 0;
    float EffectTimer = 0;

    public GameObject effectLight_b;
    public GameObject effectLight_y;
    public GameObject effectLight_g;

    public GameObject Swordeffect;

    GameObject skillGauge_rain;
    GameObject skillGauge_cloud;
    GameObject skillGauge_wind;
    Image image_rain;
    Image image_cloud;
    Image image_wind;

    private SphereCollider sphereCollider;
    //private Gurgugi gurgugi;

    private AudioSource audio;
    public AudioClip AttackSound;

    void Start()
    {
        this.skillGauge_rain = GameObject.Find("rainSkill");
        this.skillGauge_cloud = GameObject.Find("cloudSkill");
        this.skillGauge_wind = GameObject.Find("windSkill");
        image_rain = skillGauge_rain.GetComponent<Image>();
        image_cloud = skillGauge_cloud.GetComponent<Image>();
        image_wind = skillGauge_wind.GetComponent<Image>();

        weapon = GetComponent<Transform>();
        type = TYPE.RAIN;

        sphereCollider = GetComponent<SphereCollider>();

        //gurgugi = GameObject.Find("Gurguri_h").GetComponent<Gurgugi>();

        audio = GetComponent<AudioSource>();
        audio.clip = AttackSound;
    }

    void Update()
    {
        MonsterCheck();
        WeaponChange();
        WeaponEffect();
        //Kill_Monster();
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
                effectLight_b.SetActive(true);
                effectLight_g.SetActive(false);
                effectLight_y.SetActive(false);
                break;
            case TYPE.CLOUD:
                playerLight.color = new Color(1f, 0.92f, 0.08f, 1);
                effectLight_b.SetActive(false);
                effectLight_g.SetActive(false);
                effectLight_y.SetActive(true);
                break;
            case TYPE.WIND:
                playerLight.color = new Color(0.20f, 0.92f, 0.09f, 1);
                effectLight_b.SetActive(false);
                effectLight_g.SetActive(true);
                effectLight_y.SetActive(false);
                break;
        }
    }
    void MonsterCheck()
    {
        //Collider[] collisions = Physics.OverlapSphere(transform.position, 0.3f);
        //foreach (Collider collider in collisions)
        //{
        //    if (collider.gameObject.tag == "Monster")
        //    {
        //        Kill_Monster(coll);
        //        break;
        //    }
        //}
        if (attackTimer == 0)
        {
            if (Input.GetKey(KeyCode.R))
            {
                attackTimer = Time.time;
                audio.Play();
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
    public void Kill_Monster(GameObject collider)
    {
        collider.gameObject.GetComponent<Gurgugi>().TakeDamage(Damage);
    }
    //public void Overlap_Attack()
    //{
    //    Collider[] collisions = Physics.OverlapSphere(transform.position, 0.3f);
    //    foreach (Collider collider in collisions)
    //    {
    //        if (collider.gameObject.tag == "Monster")
    //        {
    //            Kill_Monster();
    //            break;
    //        }
    //    }
    //}

    public void skillGauge_Rain()
    {
        this.skillGauge_rain.GetComponent<Image>().fillAmount += 0.125f;
        if (skillGauge_rain.GetComponent<Image>().fillAmount >= 1.0f)
        {
            image_rain.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Full_rain") as Sprite;
            if (Input.GetKeyUp(KeyCode.Q))
            {
                image_rain.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_B") as Sprite;
                skillGauge_rain.GetComponent<Image>().fillAmount = 0.0f;
            }
        }

    }
    public void skillGauge_Cloud()
    {
        this.skillGauge_rain.GetComponent<Image>().fillAmount += 0.125f;
        if (skillGauge_rain.GetComponent<Image>().fillAmount == 1.0f)
        {
            image_cloud.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Full_cloud") as Sprite;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            image_cloud.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Y") as Sprite;
            skillGauge_rain.GetComponent<Image>().fillAmount -= 1.0f;
        }
    }
    public void skillGauge_Wind()
    {
        this.skillGauge_rain.GetComponent<Image>().fillAmount += 0.125f;
        if (skillGauge_rain.GetComponent<Image>().fillAmount == 1.0f)
        {
            image_wind.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Full_wind") as Sprite;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            image_wind.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_G") as Sprite;
            skillGauge_rain.GetComponent<Image>().fillAmount -= 1.0f;
        }
    }


}






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