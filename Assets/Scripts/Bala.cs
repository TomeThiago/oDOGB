using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{
    [SerializeField]
    private float speed = 0;
    [SerializeField]
    private bool paraDireita;

    // Update is called once per frame
    void Update()
    {
        if (paraDireita)
        {
            IrParaDireita();
        }
        else
        {
            IrParaEsquerda();
        }
    }

    public void SetParaDireita(bool paraDireita)
    {
        this.paraDireita = paraDireita;
    }

    public void IrParaDireita()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

    public void IrParaEsquerda()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(-speed, 0);
    }

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        Destroy(this.gameObject);
    }

}
