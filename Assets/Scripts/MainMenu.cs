using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private void Start()
    {
    }

    public void SeleccionarNivel()
    {
        // Si PlayerDataManager.Instance ya existe, significa que hay datos de guardado
        if (PlayerDataManager.Instance != null && PlayerDataManager.Instance.playerData != null)
        {
            SceneManager.LoadScene("LevelsMenu");
        }
        else
        {
            // Si no está registrado, carga la escena de registro
            SceneManager.LoadScene("LoginScene");
        }
    }

    public void SalirJuego()
    {
        Application.Quit();
    }
}
