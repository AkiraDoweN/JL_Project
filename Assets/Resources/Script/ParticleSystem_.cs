using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParticleSystem_ : MonoBehaviour
{
    public ParticleSystem Effect;
    // Start is called before the first frame update
    void Start()
    {
        Effect = GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "PlayerWeapon")
        {
            Effect.Play();
        }
    }
}
