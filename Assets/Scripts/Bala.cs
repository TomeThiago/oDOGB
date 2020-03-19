using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bala : MonoBehaviour
{

    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        GetComponent<Rigidbody2D>().velocity = new Vector2(speed, 0);
    }

    public void OnCollisionEnter2D(Collision2D collision2D)
    {
        




    }


    public void OnTriggerExit2D(Collider2D collision2D)
    {
        
    }

}
