using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine.Analytics;

public class Player : MonoBehaviour
{//ODOGB

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

    public float tempo;

    //Fazer um esquema de caso o player n esteja com a vida máxima ele ganhe vida na fase, e caso ele esteja ele ganhe uma "continue" a mais


    // Start is called before the first frame update
    void Start()
    {
        speed = 5.00f;
        jump = 650.00f;
        life = 3;
        tempobala = 2;
        velocidademaxima = 6;
        vidaMáxima = 3;
    }

    // Update is called once per frame
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
            GameObject.Instantiate(this.Bala, this.transform.position, Quaternion.identity);
        }

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
        //Debug.Log("Colidiu com" + collision2D.gameObject.tag);
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
        }

        //Define o GameObject que será o último checkpoint
        if (collision2D.gameObject.CompareTag("Checkpoint"))
        {
            LastCheckpoint = collision2D.gameObject;
        }

        //Ganhar pontos
        if (collision2D.gameObject.CompareTag("Points"))
        {
            pontuação += 1;
            Pontos.text = pontuação.ToString();
            Destroy(collision2D.gameObject);
        }

        //Regenera vida                                                                                                                                                                                                                     
        if (collision2D.gameObject.CompareTag("1up"))
        {
            //Se a vida for diferente da vida máxima o personagem ganha uma vida
            if (life != vidaMáxima)
            {
                life += 1;
                GameObject.Find("Vida" + life.ToString()).gameObject.GetComponent<Image>().enabled = true;
                Destroy(collision2D.gameObject);
            }
            //Caso contrário ele irá ganhar um ponto
            else
            {
                pontuação += 1;
                Pontos.text = pontuação.ToString();
                Destroy(collision2D.gameObject);
            }

        }

        //Apenas acontece em Triggers 
        if (collision2D.gameObject.CompareTag("HK"))
        {
            //transform.position = LastCheckpoint.transform.position;
            transform.position = resetar();
            //O personagem volta pro ultimo checkpoint
        }

    }


    public void OnTriggerExit2D(Collider2D collision2D)
    {

    }


    private void perderVida()
    {

        if (imunidade == false)
        {
            GameObject.Find("Vida" + life.ToString()).gameObject.GetComponent<Image>().enabled = false;
        }

        life -= 1;

        //Chamando a função para esperar o tempo da imunidade.
        StartCoroutine(wait());
        
        //Mecânica de morte do personagem, teleporta ele para o último checkpoint
        if (life <= 0)
        {
            //restartCurrentScene();
            transform.position = resetar();
        }

        /*void restartCurrentScene()
       {
           Scene scene = SceneManager.GetActiveScene();
           SceneManager.LoadScene(scene.name);
       }
       */
    }

    private Vector3 resetar()
    {

        life = 3;
        for(int i = vidaMáxima; i > 0; i--)
        {
            GameObject.Find("Vida" + i.ToString()).gameObject.GetComponent<Image>().enabled = true;           
        }
        return LastCheckpoint.transform.position;

    }

    //Contagem de tempo
    private IEnumerator wait()
    {
        WaitForSeconds wait = new WaitForSeconds(3f);
        imunidade = true;
        yield return wait;
        imunidade = false;
    }






}
