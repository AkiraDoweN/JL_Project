using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTimer : MonoBehaviour
{
    public float activetime;

    // Start is called before the first frame update
    void Start()
    {
        Destroy(gameObject, activetime);

    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
