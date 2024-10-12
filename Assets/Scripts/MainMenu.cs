using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SeleccionarNivel()
    {
        StartCoroutine(WaitAndLoadLevel());
    }

    private IEnumerator WaitAndLoadLevel()
    {
        yield return new WaitForSeconds(0.5f); // Espera 1 segundo
        SceneManager.LoadScene("LevelsMenu"); // Cargar la escena
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
