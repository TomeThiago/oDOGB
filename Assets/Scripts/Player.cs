using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    public float speed;
    public float jump;
    public int life;

    public bool Inground;
    public GameObject LastCheckpoint;
    public Text Pontos;

    public int pontuação;
    public GameObject Bala;
    public Vector2 posição, rotação;

    public float tempobala;
    public float timer = 0.00f;
    public float velocidademaxima;

    public bool imunidade;
    private float i;
    public int vidaMáxima;
    
    //Fazer ele ganhar imunidade após tomar dano
    //Fazer um esquema de caso o player n esteja com a vida máxima ele ganhe vida na fase, e caso ele esteja ele ganhe uma "continue" a mais


    void Start()
    {
        speed = 5.00f;
        jump = 650.00f;
        life = 3;
        tempobala = 2;
        velocidademaxima = 6;
        vidaMáxima = 3;
    }

    void Update()
    {
     //movimento horizontal
        float movimento = Input.GetAxis("Horizontal");
        Rigidbody2D rigidbody = GetComponent<Rigidbody2D>();
        rigidbody.velocity = new Vector2(movimento * speed, rigidbody.velocity.y);
     //Inversão do sprite
        if (movimento > 0)
        {
            GetComponent<SpriteRenderer>().flipX = false;
        } 
        if (movimento < 0)
        {
            GetComponent<SpriteRenderer>().flipX = true;
        }

     // Pulo do personagem
        if (Input.GetKeyDown(KeyCode.Space) && Inground == true)
        {
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, jump));
            RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 0.5f);

            Inground = hit.collider.tag == "tagPlataforma"; 
        }
     //Som do personagem andando
        if (movimento > 0 || movimento < 0)
        {
            GetComponent<Animator>().SetBool("Walking", true);
        }

        else
        {
            GetComponent<Animator>().SetBool("Walking", false);
            GetComponent<AudioSource>().Play();
            GetComponent<AudioSource>().loop = true;
        }
     //Verifica se o personagem está no chão
        if (Inground)
        {
            GetComponent<Animator>().SetBool("Pulando", false);
        }
        else
        {
            GetComponent<Animator>().SetBool("Pulando", true);
        }
        
        if (GetComponent<SpriteRenderer>().sprite.Equals("baril_pgn_6"))
        {
            Debug.Log("O barril explodiu");
        }

        // if (Input.GetKeyDown(KeyCode.E))
        //{

        // GameObject balarevolver = (GameObject) Instantiate(Bala, posição, Quaternion.Euler(rotação)) ;
        // timer = 0;

        //}
        // timer += Time.deltaTime;
        // if (timer >= tempobala)
        // {
        //     Debug.Log("Passou 2 segundos");
        //     Destroy(Bala.gameObject);
        // }
    
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 8;
        }
        else if (Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5;
        }



    }

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        Debug.Log("Colidiu com" + collision2D.gameObject.tag);
        // Testa se a tag que o player está colidindo é plataformas, se for, a variavel no chão, é verdadeira
        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            Inground = true;
        }
         
        if (collision2D.gameObject.CompareTag("Espinhos"))
        {
            perderVida();
        }
        //Printar que ele colidiu com o retangulo
        if (collision2D.gameObject.CompareTag("DanoMH"))
        {
            perderVida();
        }
        if (collision2D.gameObject.CompareTag("Monstros"))
        {
            perderVida();
        }

    }

    public void OnCollisionExit2D(Collision2D collision2D)
    {
        Debug.Log("Deixou de colidir com " + collision2D.gameObject.tag);

        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            Inground = false;
        }






    }

    public void OnTriggerEnter2D(Collider2D collision2D)
    {
        if (collision2D.gameObject.CompareTag("Armas"))
        {
            Destroy(collision2D.gameObject);
            //Transform pf = GameObject.Find("Armas").transform;
            //Transform revo = pf.Find("revorvere");
            

        }
        
        if (collision2D.gameObject.CompareTag("Checkpoint"))
        {
            LastCheckpoint = collision2D.gameObject;
        }

        if (collision2D.gameObject.CompareTag("Points"))
        {
            pontuação += 1;
            Pontos.text = pontuação.ToString();
            Destroy(collision2D.gameObject);
        }

        if (collision2D.gameObject.CompareTag("1up"))
        {
            if(life != vidaMáxima)
            {
                life += 1;
                Destroy(collision2D.gameObject);
            }
            else
            {
                pontuação += 1;
                Pontos.text = pontuação.ToString();
                Destroy(collision2D.gameObject);
            }
            if (life == 3 && imunidade == false)
            {
                // Destroy(GameObject.Find("Vida3").gameObject);
                GameObject.Find("Vida3").gameObject.GetComponent<Image>().enabled = true;
            }
            else if (life == 2 && imunidade == false)
            {
                //Destroy(GameObject.Find("Vida2").gameObject);
                GameObject.Find("Vida2").gameObject.GetComponent<Image>().enabled = true;
            }
        }

        if (collision2D.gameObject.CompareTag("HK"))
        {
            transform.position = LastCheckpoint.transform.position;
            life = 3;
            GameObject.Find("Vida").gameObject.GetComponent<Image>().enabled = true;
            GameObject.Find("Vida2").gameObject.GetComponent<Image>().enabled = true;
            GameObject.Find("Vida3").gameObject.GetComponent<Image>().enabled = true;
        }




    }


    public void OnTriggerExit2D(Collider2D collision2D)
    {
        







    }


    private void perderVida()
    {
            if(life == 3 && imunidade == false)
            {
                // Destroy(GameObject.Find("Vida3").gameObject);
                GameObject.Find("Vida3").gameObject.GetComponent<Image>().enabled = false;
            }
            else if(life == 2 && imunidade == false)
            {
                //Destroy(GameObject.Find("Vida2").gameObject);
                GameObject.Find("Vida2").gameObject.GetComponent<Image>().enabled = false;
            }
            else if(life == 1 && imunidade == false)
            {
                //Destroy(GameObject.Find("Vida").gameObject);
                GameObject.Find("Vida").gameObject.GetComponent<Image>().enabled = false;
            }

            life -= 1;
            for (i = 0; i <= 4; i += Time.deltaTime)
            {
                imunidade = true;
            }

            imunidade = false;
            if (life <= 0)
            {
                //restartCurrentScene();
                transform.position = LastCheckpoint.transform.position;
                life = 3;
                GameObject.Find("Vida").gameObject.GetComponent<Image>().enabled = true;
                GameObject.Find("Vida2").gameObject.GetComponent<Image>().enabled = true;
                GameObject.Find("Vida3").gameObject.GetComponent<Image>().enabled = true;
            }

         /*void restartCurrentScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }
        */
    }








}
