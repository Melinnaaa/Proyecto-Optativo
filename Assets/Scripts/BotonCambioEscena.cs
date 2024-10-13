using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonCambioEscena : MonoBehaviour
{




    public void CargarMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
    }
    public void Comenzar()
    {
        
        SceneManager.LoadScene("Level1");
    }
    public void proxLevel()
    {
        
        SceneManager.LoadScene("Level2");
    }
}
