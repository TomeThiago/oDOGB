using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TrocaFases : MonoBehaviour
{
    public void CarregarMenu()
    {
        SceneManager.LoadScene("Menu");
    }

    public void CarregarFase1()
    {
        SceneManager.LoadScene("Scene 1");
    }
}
