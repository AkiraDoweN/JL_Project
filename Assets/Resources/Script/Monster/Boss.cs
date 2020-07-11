using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Boss : MonoBehaviour
{
    [SerializeField]
    private float StartHp = 3000;
    [SerializeField]
    public float NowHp = 0;
    [SerializeField]
    private int takeDamage = 25;
    [SerializeField]
    private int skillTakeDamage = 200;
    [SerializeField]
    private float Knock_back_power = 1.5f;
    float[] StateTimechk = new float[3];
    public Slider hpSlider;

    private Transform Target;

    NavMeshAgent nav;
    public GameObject attack;
    Animator animtor;
    float invincibility_time = 0;

    public GameObject Boss_emergence;
    float Boss_TimeCheck;

    public ParticleSystem Effect_White;

    public float damageTimeout = 1f;
    private bool canTakeDamage = true;

    public GameObject Boss_hpSlider;

    void Start()
    {
        Target = GameObject.FindGameObjectWithTag("Player").transform;

        animtor = GetComponent<Animator>();
        nav = GetComponent<NavMeshAgent>();
        NowHp = StartHp;
        animtor.SetInteger("Boss_State", 1);
        Effect_White = GetComponent<ParticleSystem>();
        Effect_White.Stop();
    }

    private void Update()
    {
        Boss_View();
    }

    void FixedUpdate()
    {
        SetState();
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon" && Time.time - invincibility_time > 0.08f)
        {
            NowHp -= takeDamage;
            hpSlider.value = NowHp;
            animtor.SetInteger("Boss_State", 3);
            invincibility_time = Time.time;
            if (NowHp <= 0)
                Dead();
            Knock_back(other.gameObject.transform.position);
            StartCoroutine(damageTimer());
            Effect_White.Play();
        }
        
        if (other.tag == "Skill_1" && Time.time - invincibility_time > 0.32f)
        {
            NowHp -= skillTakeDamage;
            animtor.SetInteger("Boss_State", 3);
            invincibility_time = Time.time;
            if (NowHp <= 0)
                Dead();
            Knock_back(other.gameObject.transform.position);
            StartCoroutine(damageTimer());
            Effect_White.Play();
        }
        
    }

    private IEnumerator damageTimer()
    {
        canTakeDamage = false;
        yield return new WaitForSeconds(damageTimeout);
        canTakeDamage = true;
    }

    void Knock_back(Vector3 obj)
    {
        Vector3 temp = (transform.position - obj).normalized;
        transform.position += temp * Knock_back_power;
    }

    void SetState()
    {
        nav.SetDestination(transform.position);
        transform.LookAt(Player.GetInstance().gameObject.transform, Vector3.forward);
        
        switch (animtor.GetInteger("Boss_State"))
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

            case 4:
                Skill();
                break;
        }
    }

    void Move()
    {
        if ((Target.position - transform.position).magnitude < 7)
        {
            animtor.SetInteger("Boss_State", 0);
        }
        else
        {
            nav.SetDestination(Target.position);
        }
    }

    void Attack()
    {
        if (StateTimechk[0] == 0)
        {
            animtor.SetInteger("Boss_State", 0);
            StateTimechk[0] = Time.time;
            nav.speed = 0;
            //attack.SetActive(true);
        }
        else if (Time.time - StateTimechk[0] >= 0.7115385f)
        {
            nav.speed = 20.0f;
            animtor.SetInteger("Boss_State", 3);
            //attack.SetActive(false);
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
            animtor.SetInteger("Boss_State", 3);
        }
    }

    void Skill()
    {

    }
    public void Dead()
    {
        animtor.SetInteger("Boss_State", 2);
        if (StateTimechk[2] == 0)
        {
            StateTimechk[2] = Time.time;
        }
        else if (Time.time - StateTimechk[2] >= 2.0f)
        {
            SceneManager.LoadScene("Ending 1");

        }
    }


    public void Boss_View()
    {
        Boss_emergence.SetActive(true);
        Boss_hpSlider.SetActive(true);

        if (gameObject == true)
        {
            Boss_TimeCheck = Time.time;
            Debug.Log(Boss_TimeCheck);
        }
        if (Time.time - Boss_TimeCheck > 4f)
        {
            Boss_emergence.SetActive(false);
            Boss_TimeCheck = Time.time;
        }
    }
}
