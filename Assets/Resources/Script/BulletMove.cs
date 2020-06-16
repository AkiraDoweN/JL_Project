using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMove : MonoBehaviour
{
    private float speed = 60.0f;
    public float Damage = 200;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Monster")
        {
            Kill_Monster(other);
            Debug.Log("Bullet");
            
        }
        if (other.tag == "Fance")
        {
            Destroy(gameObject);
        }

    }
    public void Kill_Monster(Collider collider)
    {
        collider.gameObject.GetComponent<Gurgugi>().TakeDamage(Damage);


    }
}
