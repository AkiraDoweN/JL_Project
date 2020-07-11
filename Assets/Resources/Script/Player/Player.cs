using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 50.0f;
    public int startHp = 800;
    public int NowHp = 0;
    public int takeDamage = 10;
    public int Boss_takeDamage = 20;

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
    Vector3 Attack_look;
    public GameObject skill_cloud_vt;
    public GameObject skill_wind_vt;

    Image image_rain;
    Image image_cloud;
    Image image_wind;

    public ParticleSystem dash_Effect;

    private int BossCount;

    public GameObject attackCollider_1;
    public GameObject attackCollider_2;
    public GameObject attackCollider_3;

    Rigidbody rigidbody;

    public float damageTimeout = 1f;
    private bool canTakeDamage = true;

    public GameObject Swordeffect1;
    public GameObject Swordeffect2;
    public GameObject Swordeffect3;

    public GameObject Boss;

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
        dash_Effect = GetComponent<ParticleSystem>();

        BossCount = 0;

        rigidbody = GetComponent<Rigidbody>();
    }

    void Update()
    {
        Move();
        Dash();
        Attack();
        Skill();
        Boss_Event();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (canTakeDamage && other.tag == "MonsterWeapon")
        {
            animator.SetInteger("playerState", 3);
            NowHp -= takeDamage;
            hpSlider.value = NowHp;
            StartCoroutine(damageTimer());
            if (NowHp <= 0)
            {
                Dead();
            }
        }
        if (canTakeDamage && other.tag == "BossWeapon")
        {
            animator.SetInteger("playerState", 3);
            NowHp -= Boss_takeDamage;
            hpSlider.value = NowHp;
            StartCoroutine(damageTimer());
            if (NowHp <= 0)
            {
                Dead();
            }
        }
    }

    private IEnumerator damageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;
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
            if (Input.GetKeyDown(KeyCode.Q))
            {
                MoveSpeed = 0;
                animator.SetInteger("playerState", 8);
                BossCount += 1;
            }
        }
    }

    void Skill_cloud()
    {
        if (skillGauge_cloud.GetComponent<Image>().fillAmount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                MoveSpeed = 0;
                animator.SetInteger("playerState", 9);
                Instantiate(Skill_S, transform.position + transform.forward * 15, transform.rotation);
                Instantiate(skill_cloud_Effect, skill_cloud_vt.transform.position + transform.forward * 15, transform.rotation);
                BossCount += 1;
            }
        }
    }

    void Skill_wind()
    {
        if (skillGauge_wind.GetComponent<Image>().fillAmount >= 1)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                MoveSpeed = 0;
                animator.SetInteger("playerState", 10);
                Instantiate(Skill_D, transform.position, transform.rotation);
                Instantiate(skill_wind_Effect, skill_wind_vt.transform.position, transform.rotation);
                BossCount += 1;
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
            //transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
            Vector3 look1 =  transform.forward.normalized * MoveSpeed;
            look1.y = rigidbody.velocity.y;
            rigidbody.velocity = look1;
            MoveSpeed = 50;
            if (AttackCheck == -1)
                animator.SetInteger("playerState", 1);

        }
        else
        {
            if (AttackCheck == -1)
                animator.SetInteger("playerState", 0);
            rigidbody.velocity = new Vector3(0, rigidbody.velocity.y , 0);
        }
    }

    public void Attack()
    {
        if (AttackTime == 0 && EffectTimer == 0)
        {
            if (Input.GetKeyDown(KeyCode.R) && AttackCheck == -1)
            {
                AttackCheck = 0;
                AttackTime = Time.time;
                EffectTimer = Time.time;
                animator.SetInteger("playerState", 2);
            }
        }
        else
        {
            if (Time.time - EffectTimer > 0.05f)
            {
                Swordeffect1.SetActive(true);
                Swordeffect2.SetActive(false);
                Swordeffect3.SetActive(false);
            }
            if (Time.time - EffectTimer > 0.6f)
            {
                Swordeffect1.SetActive(false);
                Swordeffect2.SetActive(false);
                Swordeffect3.SetActive(false);
                EffectTimer = 0;
            }
            if (Time.time - AttackTime > 0.2f)
            {
                attackCollider_1.SetActive(true);
                attackCollider_2.SetActive(false);
                attackCollider_3.SetActive(false);
            }
            if (Time.time - AttackTime > 0.6f)
            {
                attackCollider_1.SetActive(false);
                attackCollider_2.SetActive(false);
                attackCollider_3.SetActive(false);
                AttackTime = 0;
            }
        }
        switch (AttackCheck)
        {
            case 0:
                FirstAttack();
                MoveSpeed = 0;
                break;
            case 1:
                SecondAttack();
                MoveSpeed = 0;
                break;
            case 2:
                ThirdAttack();
                MoveSpeed = 0;
                break;
        }
    }

    private void FirstAttack()
    {
        if (Time.time - AttackTime > 0.01 && Input.GetKeyDown(KeyCode.R))
        {
            AttackCheck = 1;
            AttackTime = Time.time;
            animator.SetInteger("playerState", 6);
        }
        if (Time.time - AttackTime > 0.9)
        {
            AttackCheck = -1;
            animator.SetInteger("playerState", 0);
            MoveSpeed = 50;
        }
    }

    private void SecondAttack()
    {

        if ( Time.time - AttackTime > 0.01 && Input.GetKeyDown(KeyCode.R))
        // if (Time.time - AttackTime > 0.08 && Input.GetKeyDown(KeyCode.R))
        {
            AttackCheck = 2;
            AttackTime = Time.time;
            //EffectTimer = Time.time;
            animator.SetInteger("playerState", 7);
        }
        if (Time.time - AttackTime > 0.6)
        //if (Time.time - AttackTime > 0.531)
        {
            AttackCheck = -1;
            animator.SetInteger("playerState", 0);
            MoveSpeed = 50;
        }
        else
        {
            if (Time.time - AttackTime > 0.1f)
            {
                attackCollider_1.SetActive(false);
                attackCollider_2.SetActive(true);
                attackCollider_3.SetActive(false);
            }
            if (Time.time - AttackTime > 0.55f)
            {
                attackCollider_1.SetActive(false);
                attackCollider_2.SetActive(false);
                attackCollider_3.SetActive(false);
                AttackTime = 0;
            }
            //if (Time.time - EffectTimer > 0.01f)
            //{
            //    Swordeffect1.SetActive(false);
            //    Swordeffect2.SetActive(true);
            //    Swordeffect3.SetActive(false);
            //}
            //if (Time.time - EffectTimer > 2f)
            //{
            //    Swordeffect1.SetActive(false);
            //    Swordeffect2.SetActive(false);
            //    Swordeffect3.SetActive(false);
            //    EffectTimer = 0;
            //}
        }
    }

    private void ThirdAttack()
    {
        if (Time.time - AttackTime > 0.6 && Input.GetKeyDown(KeyCode.R))
        {
            AttackCheck = 0;
            AttackTime = Time.time;
           // EffectTimer = Time.time;
            animator.SetInteger("playerState", 2);
        }
        if (Time.time - AttackTime > 0.7)
        {
            MoveSpeed = 50;
            AttackCheck = -1;
            animator.SetInteger("playerState", 0);
        }
        else
        {
            if (Time.time - AttackTime > 0.1f)
            {
                attackCollider_1.SetActive(false);
                attackCollider_2.SetActive(false);
                attackCollider_3.SetActive(true);
            }
            if (Time.time - AttackTime > 0.7f)
            {

                attackCollider_1.SetActive(false);
                attackCollider_2.SetActive(false);
                attackCollider_3.SetActive(false);
                AttackTime = 0;
            }
            //if (Time.time - EffectTimer > 0.05f)
            //{
            //    Swordeffect1.SetActive(false);
            //    Swordeffect2.SetActive(false);
            //    Swordeffect3.SetActive(true);
            //}
            //if (Time.time - EffectTimer > 0.6f)
            //{
            //    Swordeffect1.SetActive(false);
            //    Swordeffect2.SetActive(false);
            //    Swordeffect3.SetActive(false);
            //    EffectTimer = 0;
            //}
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
                MoveSpeed = 500;
                dash_Effect.Play();
            }
            else
            {
                MoveSpeed = 50;
                dash_Effect.Stop();
            }
        }

    }

    public void Dead()
    {
        animator.SetInteger("playerState", 5); //죽는 애니메이션
        gameOverWindow.SetActive(true);
        Time.timeScale = 0;
    }

    public void Boss_Event()
    {
        if (BossCount == 3) {
            Boss.SetActive(true);
        }
    }

   
}


