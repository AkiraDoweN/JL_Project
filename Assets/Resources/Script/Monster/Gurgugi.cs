﻿using System.Collections;
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
    NavMeshAgent nav;
    public Renderer renderer;
    public GameObject attack;

    Animator anim;
    TYPE type;

    GameObject skillGauge;

    private Transform Target;
    float[] StateTimechk = new float[3];
    void Start()
    {
        this.skillGauge = GameObject.Find("windSkill");
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
        if (other.tag == "PlayerWeapon")
        {
            NowHp -= takeDamage;
            if(NowHp <= 0)
                Dead(skillGauge);
        }
    }

    void FixedUpdate()
    {
        SetState();
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
                Dead(skillGauge);
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
            nav.speed = 3.5f;
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

    public void Dead(GameObject skillGuage)
    {
        anim.SetInteger("state", 2);
        if (StateTimechk[2] == 0)
        {
            StateTimechk[2] = Time.time;
        }
        else if (Time.time - StateTimechk[2] >= 4f)
        {
            Destroy(gameObject);
            this.skillGauge.GetComponent<Image>().fillAmount += 0.125f;
        }
    }
}
