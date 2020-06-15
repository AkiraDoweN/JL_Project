using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject bullet;
    public GameObject bulletposition;
    public float Damage = 200;

    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Skill_1();
    }

    public void Skill_1()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            GameObject bulletObject = Instantiate(bullet);
            bulletObject.transform.position = transform.position + transform.forward;
            bulletObject.transform.forward = bulletposition.transform.forward;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Kill_Monster(other);
        }
    }
    public void Kill_Monster(Collider collider)
    {
        collider.gameObject.GetComponent<Gurgugi>().TakeDamage(Damage);

    }
}
