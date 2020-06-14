using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public class Gurgugi : Monster
{
    [SerializeField]
    private int StartHp = 100;
    [SerializeField]
    private int NowHp = 0;
    [SerializeField]
    private Material[] skin;
    [SerializeField]
    private int takeDamage = 20;
    [SerializeField]
    private float Knock_back_power = 10;
    NavMeshAgent nav;
    public Renderer renderer;
    public GameObject attack;

    Animator anim;
    TYPE type;
    float invincibility_time;

    GameObject skillGauge_rain;
    GameObject skillGauge_cloud;
    GameObject skillGauge_wind;
    Image image;

    private Transform Target;
    float[] StateTimechk = new float[3];
    void Start()
    {
        this.skillGauge_rain = GameObject.Find("rainSkill");
        this.skillGauge_cloud = GameObject.Find("cloudSkill");
        this.skillGauge_wind = GameObject.Find("windSkill");
        image = skillGauge_rain.GetComponent<Image>();

        anim = GetComponent<Animator>();
        anim.SetInteger("state", 3);
        SetType();
        nav = GetComponent<NavMeshAgent>();
        Target = GameObject.FindGameObjectWithTag("Player").transform;
        NowHp = StartHp;
    }

    void SetType()
    {
        type = (TYPE)Random.Range(0, 3);
        switch (type)
        {
            case TYPE.RAIN:
                renderer.material = skin[0];
                break;
            case TYPE.CLOUD:
                renderer.material = skin[1];
                break;
            case TYPE.WIND:
                renderer.material = skin[2];
                break;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon" && Time.time - invincibility_time > 0.08f)
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

    void Knock_back(Vector3 obj)
    {
        Vector3 temp = (transform.position - obj).normalized;
        transform.position += temp * Knock_back_power;
    }


    void SetState()
    {
        nav.SetDestination(transform.position);
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
            skillGauge_Rain();
        }
    }
    public void skillGauge_Rain()
    {
        this.skillGauge_rain.GetComponent<Image>().fillAmount += 0.125f;
        if (skillGauge_rain.GetComponent<Image>().fillAmount == 1.0f)
        {
            image.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Full_rain") as Sprite;
        }
        if (Input.GetKeyUp(KeyCode.Q))
        {
            image.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_B") as Sprite;
            skillGauge_rain.GetComponent<Image>().fillAmount -= 1.0f;
        }
    }
    public void skillGauge_Cloud()
    {
        this.skillGauge_rain.GetComponent<Image>().fillAmount += 0.125f;
        if (skillGauge_rain.GetComponent<Image>().fillAmount == 1.0f)
        {
            image.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Full_cloud") as Sprite;
        }
        if (Input.GetKeyUp(KeyCode.W))
        {
            image.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Y") as Sprite;
            skillGauge_rain.GetComponent<Image>().fillAmount -= 1.0f;
        }
    }
    public void skillGauge_Wind()
    {
        this.skillGauge_rain.GetComponent<Image>().fillAmount += 0.125f;
        if (skillGauge_rain.GetComponent<Image>().fillAmount == 1.0f)
        {
            image.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_Full_wind") as Sprite;
        }
        if (Input.GetKeyUp(KeyCode.E))
        {
            image.sprite = Resources.Load<Sprite>("UI/Game/Skill_dash/JL_UI_skill_G") as Sprite;
            skillGauge_rain.GetComponent<Image>().fillAmount -= 1.0f;
        }
    }
}
