using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{  
    public void JugarNivel1()
    {
        SceneManager.LoadScene("Level1");
    }

    public void JugarNivel2()
    {
        SceneManager.LoadScene("Level2");
    }

    public void JugarNivel3()
    {
        SceneManager.LoadScene("Level3");
    }

    public void Volver()
    {
        SceneManager.LoadScene("MainMenu");
    }
}
