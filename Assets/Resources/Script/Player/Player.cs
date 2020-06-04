using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int MoveSpeed = 40;
    public int startHp = 800;
    public int NowHp = 0;
    public int takeDamage = 10;

    float WaitingTime = 2.0f;
    float timer = 0.0f;
    float HorizontalMove;
    float VerticalMove;

    private Animator animator;
    public GameObject gameOverWindow;

    public Slider hpSlider;
    // public GameObject Swordeffect;


    float AttackTime = 0;
    int AttackCheck = -1;

    //public ParticleSystem weaponParticle;

    Vector3 look;

    void Start()
    {
        animator = GetComponent<Animator>();
        //weaponParticle = GetComponent<ParticleSystem>();
        NowHp = startHp;
    }

    void Update()
    {
        Move();
        Dash();
        Attack();
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
    public void Move()
    {
         VerticalMove = Input.GetAxisRaw("Vertical");
         HorizontalMove = Input.GetAxisRaw("Horizontal");
         if (VerticalMove + HorizontalMove != 0)
         {
            look = new Vector3(HorizontalMove, 0, VerticalMove);

            transform.rotation = Quaternion.LookRotation(look);
            transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
            if(AttackCheck == -1)
                animator.SetInteger("playerState", 1);
            
            if (Input.GetKey(KeyCode.R))
            {
                MoveSpeed = 0;
            }
            else
            {
                MoveSpeed = 40;
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
            //Swordeffect.SetActive(true);
        }
        //else
        //{
        //    Swordeffect.SetActive(false);
        //}
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
            //Swordeffect.SetActive(true);
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
            //Swordeffect.SetActive(true);
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
                MoveSpeed = 300;
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


