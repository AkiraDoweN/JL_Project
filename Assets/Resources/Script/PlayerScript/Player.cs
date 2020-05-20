using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float MoveSpeed = 20.0f;
    float HorizontalMove;
    float VerticalMove;

    private Animator animator;

    Vector3 look;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D))
        {
            VerticalMove = Input.GetAxisRaw("Vertical");
            HorizontalMove = Input.GetAxisRaw("Horizontal");
            look = VerticalMove * Vector3.forward + HorizontalMove * Vector3.right;
            this.transform.rotation = Quaternion.LookRotation(look);
            this.transform.Translate(Vector3.forward * MoveSpeed * Time.deltaTime);
            animator.SetInteger("playerState", 1);
        }
        else if (Input.GetKey(KeyCode.C))
        {
            animator.SetInteger("playerState", 2);
        }
        else
        {
            animator.SetInteger("playerState", 0);
        }
    }
}
