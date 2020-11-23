using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    private Player player = null;
    private int pontuacao = 0;
    [SerializeField]
    private GameObject painelPausado = null;
    public bool IsPause = false;
    InterfaceGrafica interfaceGrafica = null;

    private void Start()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.interfaceGrafica = GameObject.FindObjectOfType<InterfaceGrafica>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.P))
        {
            Pausar();
        }
    }

    public void Pausar()
    {
        GameObject.Find("GameManager").GetComponent<AudioSource>().mute = !IsPause;
        GameObject.Find("Player").GetComponent<AudioSource>().mute = !IsPause;
        painelPausado.gameObject.SetActive(!IsPause);
        Time.timeScale = IsPause ? 1 : 0;
        IsPause = !IsPause;
    }

    public void GanharPontoOuVida()
    {

        //Se a vida for diferente da vida máxima o personagem ganha uma vida
        if (player.Life != player.vidaMáxima)
        {
            player.GanharVida(1);
            interfaceGrafica.AtualizarInterface();
        }
        //Caso contrário ele irá ganhar um ponto
        else
        {
            AdicionarPontos(1);
            interfaceGrafica.AtualizarInterface();
        }

    }

    public void AdicionarPontos(int pontos)
    {
        this.pontuacao += pontos;
    }

    public int GetPontuacao()
    {
        return this.pontuacao;
    }
}
