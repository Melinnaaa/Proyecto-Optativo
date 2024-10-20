using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelsMenu : MonoBehaviour
{
    public void JugarNivel1()
    {
        StartCoroutine(WaitAndLoadScene("ObjetivoLvl1"));
    }

    public void JugarNivel2()
    {
        StartCoroutine(WaitAndLoadScene("Level2"));
    }

    public void JugarNivel3()
    {
        StartCoroutine(WaitAndLoadScene("Level3"));
    }

    public void Volver()
    {
        StartCoroutine(WaitAndLoadScene("MainMenu"));
    }

    private IEnumerator WaitAndLoadScene(string sceneName)
    {
        yield return new WaitForSeconds(0.5f); // Espera 0.5 segundos
        SceneManager.LoadScene(sceneName); // Cargar la escena
    }
}
