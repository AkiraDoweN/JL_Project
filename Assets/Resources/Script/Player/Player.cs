using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{
    public int MoveSpeed = 40;
    public int startHp = 800;
    public int NowHp = 0;
    public int takeDamage = 50;

    float WaitingTime = 2.0f;
    float timer = 0.0f;
    float HorizontalMove;
    float VerticalMove;

    private Animator animator;
    static private Player _player;
    public GameObject gameOverWindow;

    public Slider hpSlider;

    public GameObject Skill_S;
    public GameObject Skill_D;

    float AttackTime = 0;
    int AttackCheck = -1;

    Vector3 look;

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
        NowHp = startHp;
        animator = GetComponent<Animator>();
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
            if (NowHp <= 0)
            {
                Dead();
            }
        }
    }
    
    void Skill()
    {
        Skill_a();
        Skill_s();
        Skill_d();
    }
    public void Skill_a()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            animator.SetInteger("playerState", 8);
        }
    }


    void Skill_s()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            animator.SetInteger("playerState", 9);
            Instantiate(Skill_S, transform.position + transform.forward * 15, transform.rotation);
        }
    }

    void Skill_d()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            animator.SetInteger("playerState", 10);
            Instantiate(Skill_D, transform.position, transform.rotation);
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
            else if (Input.GetKey(KeyCode.A))
            {
                MoveSpeed = 0;
            }
            else if(Input.GetKey(KeyCode.S))
            {
                MoveSpeed = 0;
            }
            else if(Input.GetKey(KeyCode.D))
            {
                MoveSpeed = 0;
            }
            else
            {
                MoveSpeed = 40;
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
                MoveSpeed = 400;
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


