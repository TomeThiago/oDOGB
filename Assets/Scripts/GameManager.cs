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

        if (Input.GetKeyDown(KeyCode.P))
        {

            GameObject.Find("GameManager").GetComponent<AudioSource>().mute = !IsPause;
            GameObject.Find("Player").GetComponent<AudioSource>().mute = !IsPause;
            GameObject.Find("Pause").GetComponent<SpriteRenderer>().enabled = !IsPause;
            Time.timeScale = IsPause ? 1 : 0;
            IsPause = !IsPause;

        }
    }


}
