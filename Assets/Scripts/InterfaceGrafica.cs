using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InterfaceGrafica : MonoBehaviour
{
    private Player player = null;
    [SerializeField]
    private Text texto = null;
    private GameManager gameManager = null;

    // Start is called before the first frame update
    void Start()
    {
        this.player = GameObject.FindObjectOfType<Player>();
        this.gameManager = GameObject.FindObjectOfType<GameManager>();
    }

    public void AtualizarInterface()
    {
        GameObject.Find("Vida" + player.Life.ToString()).gameObject.GetComponent<Image>().enabled = true;
        texto.text = gameManager.GetPontuacao().ToString(); 
    }
}
