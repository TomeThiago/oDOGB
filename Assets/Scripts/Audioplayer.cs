using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Audioplayer : MonoBehaviour
{
    public AudioSource[] _audio = new AudioSource[5];

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _audio[0].Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            _audio[1].Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            _audio[2].Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha3))
        {
            _audio[3].Play();
        }

        if (Input.GetKeyDown(KeyCode.Alpha4))
        {
            _audio[4].Play();
        }
    }
}
