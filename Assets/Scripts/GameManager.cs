using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public GameObject Painelpausado;
    public bool IsPause = false;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (IsPause == false && Input.GetKeyDown(KeyCode.P))
        {
            Painelpausado.SetActive(true);
            Time.timeScale = 0;
            GameObject.Find("GameManager").GetComponent<AudioSource>().mute = true;
            IsPause = true;
        }
        else if (IsPause == true && Input.GetKeyDown(KeyCode.P))
        {
            Painelpausado.SetActive(false);
            Time.timeScale = 1;
            GameObject.Find("GameManager").GetComponent<AudioSource>().mute = false;
            IsPause = false;
        }
        


        //if (Input.GetKeyDown(KeyCode.E))
      //  {
           // GameObject cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            //cube.transform.position = new Vector2(0, 0);
       // }



    }


}
