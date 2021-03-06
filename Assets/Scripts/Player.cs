﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Player : MonoBehaviour
{//ODOGB

    private float speed;
    private float jump;
    public int Life{ get; private set;}

    public bool Inground;
    public GameObject LastCheckpoint;
    GameManager gameManager = null;

    public int pontuação;
    public GameObject Bala;
    public GameObject arma;
    public Vector2 posição, rotação;

    public float tempobala;
    public float timer = 0.00f;
    public float velocidademaxima;

    public bool imunidade;
    public int vidaMáxima;

    public float tempo;
    //OffSets para poder fazer um RayCasting que não colida com a próprio box collider do player
    private float offSetX = 0.969f;
    private float offSetY = 1.86f;
    

    //Fazer um esquema de caso o player n esteja com a vida máxima ele ganhe vida na fase, e caso ele esteja ele ganhe uma "continue" a mais

    void Start()
    {
        speed = 5.00f;
        jump = 650.00f;
        Life = 3;
        tempobala = 2;
        velocidademaxima = 6;
        vidaMáxima = 3;

        this.gameManager = GameObject.FindObjectOfType<GameManager>();
        
    }
    
    // Update is called once per frame
    private void Update()
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
            rigidbody.velocity = Vector2.zero;
            rigidbody.AddForce(new Vector2(0, jump));
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
            //Debug.Log("O barril explodiu");
        }

        //Criar a bala
        if (Input.GetKeyDown(KeyCode.E))
        {
            //OffSet feito para o personagem n ser empurrado pela bala quando ela for instânciada
            Vector3 offsetPlayer = new Vector3(this.transform.position.x + 1.5f, this.transform.position.y, this.transform.position.z);
            GameObject.Instantiate(this.Bala, offsetPlayer, Quaternion.identity);
        }

        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            speed = 8;
        }
        else if(Input.GetKeyUp(KeyCode.LeftShift))
        {
            speed = 5;
        }
    }

    //Código de RayCast 
    //Depois fazer uma função pra resumir esse carai aqui
    private void FixedUpdate()
    {
        RaycastHit2D hit;
        float dist = 0.5f;
        Vector2 vetor = new Vector2(this.transform.position.x, this.transform.position.y - 1.86f);
        hit = Physics2D.Raycast(vetor, Vector2.down, dist);
        Debug.DrawRay(vetor, hit.point);
        if (hit.collider != null)
        {
            //Debug.Log(hit.collider.tag);
            //Debug.Log(hit.collider.transform.position);
            if (hit.collider.CompareTag("Monstros"))
            {
                try{
                    InimigoVertical inimigo = hit.collider.gameObject.GetComponent<InimigoVertical>();
                    inimigo.PerderVida(1);
                }
                catch (NullReferenceException)
                {
                    InimigoHorizontal inimigo = hit.collider.gameObject.GetComponent<InimigoHorizontal>();
                    inimigo.PerderVida(1);
                }
                
                Debug.Log("Colidiu");
            }
            
        }
        
    }

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        //Debug.Log("Colidiu com" + collision2D.gameObject.tag);
        // Testa se a tag que o player está colidindo é plataformas, se for, a variavel no chão, é verdadeira
        if (collision2D.gameObject.CompareTag("Plataformas"))
        {
            Inground = true;
        }

        if (collision2D.gameObject.CompareTag("Espinhos"))
        {
            PerderVida();
        }
        //Printar que ele colidiu com o retangulo
        if (collision2D.gameObject.CompareTag("Monstros"))
        {
            PerderVida();
        }

    }

    public void OnCollisionExit2D(Collision2D collision2D)
    {
        //"Descolisão" do chão
        //Debug.Log("Deixou de colidir com " + collision2D.gameObject.tag);

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
            GameObject.Instantiate(this.arma, this.transform.position, Quaternion.identity);
        }

        //Define o GameObject que será o último checkpoint
        if (collision2D.gameObject.CompareTag("Checkpoint"))
        {
            LastCheckpoint = collision2D.gameObject; 
        } 

        //Regenera vida                                                                                                                                                                                                                     
        if (collision2D.gameObject.CompareTag("1up"))
        {

            gameManager.GanharPontoOuVida();
            Destroy(collision2D.gameObject);

        }

        //Apenas acontece em Triggers 
        if (collision2D.gameObject.CompareTag("HK"))
        {
            //transform.position = LastCheckpoint.transform.position;
            transform.position = Resetar();
            //O personagem volta pro ultimo checkpoint
        }

    }

    private void PerderVida()
    {

        if (imunidade == false)
        {
            GameObject.Find("Vida" + Life.ToString()).gameObject.GetComponent<Image>().enabled = false;
            Life -= 1;
        }

        //Chamando a função para esperar o tempo da imunidade.
        StartCoroutine(Wait());
        
        //Mecânica de morte do personagem, teleporta ele para o último checkpoint
        if (Life <= 0)
        {
            transform.position = Resetar();
        }

        /*void restartCurrentScene()
        {
            Scene scene = SceneManager.GetActiveScene();
            SceneManager.LoadScene(scene.name);
        }*/
       
    }

    private Vector3 Resetar()
    {

        Life = vidaMáxima;
        for(int i = vidaMáxima; i > 0; i--)
        {
            GameObject.Find("Vida" + i.ToString()).gameObject.GetComponent<Image>().enabled = true;           
        }
        return LastCheckpoint.transform.position;

    }

    //Contagem de tempo
    private IEnumerator Wait()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        imunidade = true;
        yield return wait;
        imunidade = false;
    }

    public void GanharVida(int vida)
    {
        this.Life += vida;
    }

}
