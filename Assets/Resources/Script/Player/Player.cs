using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{

    public int MoveSpeed = 20;
    public int startHp = 100;
    public int NowHp = 0;
    public int takeDamage = 10;

    float WaitingTime = 2.0f;
    float timer = 0.0f;
    float HorizontalMove;
    float VerticalMove;

    private Animator animator;
    public GameObject gameOverWindow;

    public Slider hpSlider;

    private bool attackCheck_2 = false;
    private float checkAttackTime = 1f;


    Vector3 look;

    void Start()
    {
        animator = GetComponent<Animator>();
        NowHp = startHp;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow))
        {
            timer += Time.deltaTime;
            VerticalMove = Input.GetAxisRaw("Vertical");
            HorizontalMove = Input.GetAxisRaw("Horizontal");
            look = VerticalMove * Vector3.forward + HorizontalMove * Vector3.right;
            this.transform.rotation = Quaternion.LookRotation(look);
            this.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
            animator.SetInteger("playerState", 1);

            if (Input.GetKey(KeyCode.Space)) // dash
            {
                if (timer > WaitingTime)
                {
                    animator.SetInteger("playerState", 4);
                    timer = 0.0f;
                    MoveSpeed = 40;
                }
            }
            else if (Input.GetKey(KeyCode.R))
            {
                animator.SetInteger("playerState", 2);
                MoveSpeed = 0;
                if (Input.GetKeyUp(KeyCode.R))
                {
                    attackCheck_2 = true;
                    if (attackCheck_2 == true)
                    {
                        checkAttackTime -= Time.deltaTime;
                        animator.SetBool("Attack2", true);
                        if (checkAttackTime <= 0)
                        {
                            attackCheck_2 = false;
                            checkAttackTime = 0.5f;
                        }
                    }
                }
            }
            else
            {
                MoveSpeed = 20;
            }
        }
        else if (Input.GetKey(KeyCode.R))
           {
            animator.SetInteger("playerState", 2);
            MoveSpeed = 0;
        }
        else
        {
            animator.SetInteger("playerState", 0);
            MoveSpeed = 20;

        }
    }

    private void OnTriggerEnter(Collider other)
    {
       
        if (other.tag == "Monster")
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


    public void Dead()
    {
        animator.SetInteger("playerState", 5); //죽는 애니메이션
        gameOverWindow.SetActive(true);
        Time.timeScale = 0;
    }
}


