using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoVertical : Inimigo
{

    private float time = 0.00f;
    public float timer;
    public float speed;

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(0, speed);
        time += Time.deltaTime;
        if (time >= timer)
        {
            time = 0.00f;
            Flip();
        }
    }

    private void Flip()
    {
        speed *= -1;
    }

}

