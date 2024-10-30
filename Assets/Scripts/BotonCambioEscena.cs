using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BotonCambioEscena : MonoBehaviour
{




    public void CargarMenu()
    {
        
        SceneManager.LoadScene("MainMenu");
    }
    public void ComenzarLvl1()
    {
        
        SceneManager.LoadScene("Level1");
    }
    public void proxLevel()
    {
        
        SceneManager.LoadScene("Level2");
    }

    public void ComenzarLvl2()
    {
        
        SceneManager.LoadScene("Level2");
    }
}
