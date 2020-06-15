using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Gurgugi : Monster
{
    [SerializeField]
    private float StartHp = 100;
    [SerializeField]
    public float NowHp = 0;
    [SerializeField]
    private Material[] skin;
    [SerializeField]
    private int takeDamage = 200;
    [SerializeField]
    private float Knock_back_power = 10;
    NavMeshAgent nav;
    public Renderer renderer;
    public GameObject attack;

    Animator anim;
    TYPE type;
    float invincibility_time = 0;

    GameObject skillGauge_rain;
    GameObject skillGauge_cloud;
    GameObject skillGauge_wind;
    Image image_rain;
    Image image_cloud;
    Image image_wind;

    private Transform Target;
    float[] StateTimechk = new float[3];
    public bool Monster_Rain;
    public bool Monster_Cloud;
    public bool Monster_Wind;

    private GameObject bullet;
    private GameObject bulletposition;

    void Start()
    {
        this.skillGauge_rain = GameObject.Find("rainSkill");
        this.skillGauge_cloud = GameObject.Find("cloudSkill");
        this.skillGauge_wind = GameObject.Find("windSkill");
        image_rain = skillGauge_rain.GetComponent<Image>();
        image_cloud = skillGauge_cloud.GetComponent<Image>();
        image_wind = skillGauge_wind.GetComponent<Image>();

        anim = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        anim.SetInteger("state", 3);
        NowHp = StartHp;
        SetType();

        //bullet = Instantiate(Resources.Load("Prefabs/Skill_Rain_Bullet")) as GameObject;
        //this.bulletposition = GameObject.Find("Skill_Rain");
    }

    void SetType()
    {
        type = (TYPE)Random.Range(0, 3);
        switch (type)
        {
            case TYPE.RAIN:
                renderer.material = skin[0];
                Monster_Rain = true;
                Monster_Cloud = false;
                Monster_Wind = false;
                break;
            case TYPE.CLOUD:
                renderer.material = skin[1];
                Monster_Rain = false;
                Monster_Cloud = true;
                Monster_Wind = false;
                break;
            case TYPE.WIND:
                renderer.material = skin[2];
                Monster_Rain = false;
                Monster_Cloud = false;
                Monster_Wind = true;
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //if (other.tag == "PlayerWeapon" && Time.time - invincibility_time > 0.08f)
        //{
        //    NowHp -= takeDamage;
        //    anim.SetInteger("state", 1);
        //    invincibility_time = Time.time;
        //    if (NowHp <= 0)
        //        Dead();
        //    Knock_back(other.gameObject.transform.position);
        //}
        if (other.tag == "Skill_1" && Time.time - invincibility_time > 0.32f)
        {
            NowHp -= takeDamage;
            anim.SetInteger("state", 1);
            invincibility_time = Time.time;
            if (NowHp <= 0)
                Dead();
            Knock_back(other.gameObject.transform.position);
        }

    }

    void FixedUpdate()
    {
        SetState();
    }
    private void Update()
    {
        skillGauge_Rain_skill();
        skillGauge_Cloud_skill();
        skillGauge_Wind_skill();
    }

    void Knock_back(Vector3 obj)
    {
        Vector3 temp = (transform.position - obj).normalized;
        transform.position += temp * Knock_back_power;
    }

    public void TakeDamage(float amount)
    {
        if (Time.time - invincibility_time > 0.32f)
        {
            NowHp -= amount;
            anim.SetInteger("state", 1);
            invincibility_time = Time.time;
            Knock_back(gameObject.transform.position);
        }
        if (NowHp <= 0)
        {
            Dead();
        }
    }

    void SetState()
    {
        nav.SetDestination(transform.position);
        transform.LookAt(Player.GetInstance().gameObject.transform, Vector3.forward);
        switch (anim.GetInteger("state"))
        {
            case 0:
                Attack();
                break;
            case 1:
                Hit();
                break;
            case 2:
                Dead();
                break;
            case 3:
                Move();
                break;
        }
    }

    void Move()
    {
        if ((Target.position - transform.position).magnitude < 7)
        {
            anim.SetInteger("state", 0);
        }
        else
        {
            nav.SetDestination(Target.position);
        }
    }

    void Attack()
    {
        if(StateTimechk[0] == 0)
        {
            StateTimechk[0] = Time.time;
            nav.speed = 0;
            attack.SetActive(true);
        }
        else if(Time.time - StateTimechk[0] >= 0.7115385f)
        {
            nav.speed = 30f;
            anim.SetInteger("state", 3);
            attack.SetActive(false);
        }
        nav.SetDestination(Target.position);
    }
    
    void Hit()
    {
        if (StateTimechk[1] == 0)
        {
            StateTimechk[1] = Time.time;
        }
        else if (Time.time - StateTimechk[1] >= 0.53125f)
        {
            anim.SetInteger("state", 3);
        }
    }

    public void Dead()
    {
        anim.SetInteger("state", 2);
        if (StateTimechk[2] == 0)
        {
            StateTimechk[2] = Time.time;
        }
        else if (Time.time - StateTimechk[2] >= 0.5f)
        {
            Destroy(gameObject);
        }
        if (Monster_Rain == true)
        {
            skillGauge_Rain();
        }
        else if (Monster_Cloud == true)
        {
            skillGauge_Cloud();
        }
        else if (Monster_Wind == true)
        {
            skillGauge_Wind();
        }
    }
    public void skillGauge_Rain()
    {
        this.skillGauge_rain.GetComponent<Image>().fillAmount += 0.125f;
    }
    public void skillGauge_Rain_skill()
    {
        if (skillGauge_rain.GetComponent<Image>().fillAmount >= 1)
        {
            image_rain.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Full_rain") as Sprite;
            if (Input.GetKeyUp(KeyCode.Q))
            {
                image_rain.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_B") as Sprite;
                skillGauge_rain.GetComponent<Image>().fillAmount = 0;
                GameObject bulletObject = Instantiate(bullet);
                bulletObject.transform.position = transform.position + transform.forward;
                bulletObject.transform.forward = bulletposition.transform.forward;
            }
        }
    }
    public void skillGauge_Cloud()
    {
        this.skillGauge_cloud.GetComponent<Image>().fillAmount += 0.125f;
       
    }
    public void skillGauge_Cloud_skill()
    {
        
        if (skillGauge_cloud.GetComponent<Image>().fillAmount >= 1.0f)
        {
            image_cloud.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Full_cloud") as Sprite;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            image_cloud.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Y") as Sprite;
            skillGauge_cloud.GetComponent<Image>().fillAmount = 0;
        }
    }
    public void skillGauge_Wind()
    {
        this.skillGauge_wind.GetComponent<Image>().fillAmount += 0.125f;
    }
    public void skillGauge_Wind_skill()
    {
        if (skillGauge_wind.GetComponent<Image>().fillAmount >= 1.0f)
        {
            image_wind.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Full_wind") as Sprite;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            image_wind.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_G") as Sprite;
            skillGauge_wind.GetComponent<Image>().fillAmount = 0;
        }
    }
}
