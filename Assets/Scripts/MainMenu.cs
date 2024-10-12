using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public void SeleccionarNivel()
    {
        SceneManager.LoadScene("LevelsMenu");
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
