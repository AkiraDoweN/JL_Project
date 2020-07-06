using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Opening : MonoBehaviour
{
    float StartTimer = 0;
    private SpriteRenderer spriteRenderer;
    public Sprite[] sprites;

    // Start is called before the first frame update
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprites[0];

    }

    // Update is called once per frame
    void Update()
    {
       
    }

    public void Opening_Start()
    {
        //if(StartTimer == 3)
        //{
        //    image.sprite = Resources.Load<Sprite>("UI/Opening/JL_opening_ver.2") as Sprite;

        //}
    }
}
