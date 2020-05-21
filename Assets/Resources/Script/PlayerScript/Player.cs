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

    float WaitingTime = 2.0f;
    float timer = 0.0f;
    float HorizontalMove;
    float VerticalMove;

    private Animator animator;
    public Slider hpSlider;

    Vector3 look;

    enum Property //속성
    {
        Blue, //비
        Yellow, //구름
        Green,  //바람
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        NowHp = startHp;
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
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
                    timer = 0.0f;
                    MoveSpeed = 40;
                }
            }
            else
                MoveSpeed = 20;
        }   
        else if (Input.GetKey(KeyCode.C))
        {
            animator.SetInteger("playerState", 2);
        }

        else
        {
            animator.SetInteger("playerState", 0);
            MoveSpeed = 20;
        }
    }

    public void TakeDamage(int damage)
    {
        animator.SetInteger("playerState", 3);
        NowHp -= damage;
        hpSlider.value = NowHp;
        if(NowHp <= 0)
        {
            Dead();
        }
    }
    public void Dead()
    {
        //animator.SetInteger("playerState", 4); //죽는 애니메이션

    }
}
