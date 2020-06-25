using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int MoveSpeed = 50;
    public int startHp = 800;
    public int NowHp = 0;
    public int takeDamage = 50;

    float WaitingTime = 2.0f;
    float timer = 0.0f;
    float HorizontalMove;
    float VerticalMove;

    private Animator animator;
    static private Player _player;
    
    public Slider hpSlider;

    public GameObject gameOverWindow;
    public GameObject Skill_S;
    public GameObject Skill_D;

    GameObject skillGauge_rain;
    GameObject skillGauge_cloud;
    GameObject skillGauge_wind;

    public GameObject skill_cloud_Effect;
    public GameObject skill_wind_Effect;


    float AttackTime = 0;
    float EffectTimer = 0;
    int AttackCheck = -1;

    Vector3 look;
    public GameObject skill_cloud_vt;
    public GameObject skill_wind_vt;

    Image image_rain;
    Image image_cloud;
    Image image_wind;
    

    public static Player GetInstance()
    {
        return _player;
    }

    private void Awake()
    {
        _player = this;
    }

    void Start()
    {
        this.skillGauge_rain = GameObject.Find("rainSkill");
        this.skillGauge_cloud = GameObject.Find("cloudSkill");
        this.skillGauge_wind = GameObject.Find("windSkill");
        NowHp = startHp;
        animator = GetComponent<Animator>();
        image_rain = skillGauge_rain.GetComponent<Image>();
        image_cloud = skillGauge_cloud.GetComponent<Image>();
        image_wind = skillGauge_wind.GetComponent<Image>();
    }

    void Update()
    {
        Move();
        Dash();
        Attack();
        Skill();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "MonsterWeapon")
        {
            animator.SetInteger("playerState", 3);
            NowHp -= takeDamage;
            hpSlider.value = NowHp;
            //Hit_Effect.SetActive(true);

            if (NowHp <= 0)
            {
                Dead();
            }
        }
        else
        {
            //Hit_Effect.SetActive(false);

        }
    }
    
    void Skill()
    {
        Skill_rain();
        Skill_cloud();
        Skill_wind();
    }
    public void Skill_rain()
    {
        if (skillGauge_rain.GetComponent<Image>().fillAmount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                animator.SetInteger("playerState", 8);
            }
        }
    }


    void Skill_cloud()
    {
        if (skillGauge_cloud.GetComponent<Image>().fillAmount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                animator.SetInteger("playerState", 9);
                Instantiate(Skill_S, transform.position + transform.forward * 15, transform.rotation);
                Instantiate(skill_cloud_Effect, skill_cloud_vt.transform.position + transform.forward * 15, transform.rotation);
            }
        }
    }

    void Skill_wind()
    {
        if (skillGauge_wind.GetComponent<Image>().fillAmount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                animator.SetInteger("playerState", 10);
                Instantiate(Skill_D, transform.position, transform.rotation);
                Instantiate(skill_wind_Effect, skill_wind_vt.transform.position, transform.rotation);
            }
        }
    }

    public void Move()
    {
         VerticalMove = Input.GetAxisRaw("Vertical");
         HorizontalMove = Input.GetAxisRaw("Horizontal");
         if (VerticalMove != 0 || HorizontalMove != 0)
         {
            look = new Vector3(HorizontalMove, 0, VerticalMove);

            transform.rotation = Quaternion.LookRotation(look);
            transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
            if(AttackCheck == -1)
                animator.SetInteger("playerState", 1);

            if (Input.GetKey(KeyCode.R))
            {
                MoveSpeed = 0;
                GetComponent<AudioSource>().Play();
            }
            else if (Input.GetKey(KeyCode.Alpha1))
            {
                MoveSpeed = 0;
            }
            else if(Input.GetKey(KeyCode.Alpha2))
            {
                MoveSpeed = 0;
            }
            else if(Input.GetKey(KeyCode.Alpha3))
            {
                MoveSpeed = 0;
            }
            else
            {
                MoveSpeed = 50;
                GetComponent<AudioSource>().Stop();
            }
        }
        else
        {
            if (AttackCheck == -1)
                animator.SetInteger("playerState", 0);
        }
    }

    public void Attack()
    {

        if (Input.GetKeyDown(KeyCode.R) && AttackCheck == -1)
        {
            AttackCheck = 0;
            AttackTime = Time.time;
            animator.SetInteger("playerState", 2);
        }
        
        switch (AttackCheck)
        {
            case 0:
                FirstAttack();
                break;
            case 1:
                SecondAttack();
                break;
            case 2:
                ThirdAttack();
                break;
        }
    }

    private void  FirstAttack()
    {
        if (Time.time - AttackTime > 0.08 && Input.GetKeyDown(KeyCode.R))
        {
            AttackCheck = 1;
            AttackTime = Time.time;
            animator.SetInteger("playerState", 6);
        }
        if (Time.time - AttackTime > 0.51)
        {
            AttackCheck = -1;
            animator.SetInteger("playerState", 0);
        }
    }

    private void SecondAttack()
    {
        if (Time.time - AttackTime > 0.08 && Input.GetKeyDown(KeyCode.R))
        {
            AttackCheck = 2;
            AttackTime = Time.time;
            animator.SetInteger("playerState", 7);
        }
        if (Time.time - AttackTime > 0.531)
        //if (Time.time - AttackTime > 0.531)
        {
            AttackCheck = -1;
            animator.SetInteger("playerState", 0);

        }
    }

    private void ThirdAttack()
    {
        if (Time.time - AttackTime > 0.625)
        {
            AttackCheck = -1;
            animator.SetInteger("playerState", 0);
        }
    }

    public void Dash()
    {
        timer += Time.deltaTime;

        if (Input.GetKey(KeyCode.Space))
        {
            if (timer > WaitingTime)
            {
                animator.SetInteger("playerState", 4);
                timer = 0.0f;
                MoveSpeed = 1000;
            }
        }
    }

    public void Dead()
    {
        animator.SetInteger("playerState", 5); //죽는 애니메이션
        gameOverWindow.SetActive(true);
        //Time.timeScale = 0;
    }

   
}


