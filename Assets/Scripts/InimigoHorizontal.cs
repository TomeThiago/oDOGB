using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InimigoHorizontal : MonoBehaviour
{
    private float time = 0.00f;
    public float timer;
    public float speed;

    public float lifemonster;
    private Player player;


    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindObjectOfType<Player>();
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
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
        GetComponent<SpriteRenderer>().flipX = !GetComponent<SpriteRenderer>().flipX;
    }

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        // Faz que a bala ao bater no inimigo destrua o monstro
        if (collision2D.gameObject.CompareTag("Balarevolver"))
        {
            lifemonster--;
            Destroy(collision2D.gameObject);
            //Destroi o monstro
            if (lifemonster <= 0)
            {
                Destroy(gameObject);
            }
        }
    }

    public void PerderVida(int vidaPerdida)
    {
        this.lifemonster -= vidaPerdida;
        player.gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        player.gameObject.GetComponent<Rigidbody2D>().AddForce(new Vector2(0, 500));
        if (lifemonster <= 0)
        {
            Destroy(gameObject);
        }
    }

}


