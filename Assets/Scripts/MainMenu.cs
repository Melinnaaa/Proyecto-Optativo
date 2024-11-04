using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public void SeleccionarNivel()
    {
      
            // Si no está registrado, carga la escena de registro
        StartCoroutine(WaitAndLoadLevel("LoginScene"));
      
    }

    private IEnumerator WaitAndLoadLevel(string sceneName)
    {
        yield return new WaitForSeconds(0.1f); 
        SceneManager.LoadScene(sceneName); // Carga la escena especificada
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
