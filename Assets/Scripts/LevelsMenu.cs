using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelsMenu : MonoBehaviour
{
    public Button level1Button;
    public Button level2Button;
    public Button level3Button;

    private void Start()
    {
        if (PlayerDataManager.Instance == null || PlayerDataManager.Instance.playerData == null)
        {
            Debug.LogError("PlayerDataManager.Instance o playerData es null");
            return;
        }

        // Verifica el progreso del jugador y habilita o deshabilita los botones según corresponda
        UpdateLevelAccess();
    }

    private void UpdateLevelAccess()
    {
        // Asegúrate de que los botones están asignados antes de modificar su estado
        if (level1Button != null) level1Button.interactable = true;
        if (level2Button != null) level2Button.interactable = PlayerDataManager.Instance.playerData.level1Completed;
        if (level3Button != null) level3Button.interactable = PlayerDataManager.Instance.playerData.level2Completed;
    }

    public void JugarNivel1()
    {
        LoadScene("ObjetivoLvl1");
    }

    public void JugarNivel2()
    {
        if (PlayerDataManager.Instance.playerData.level1Completed)
        {
            LoadScene("ObjetivoLvl2");
        }
        else
        {
            Debug.LogWarning("El nivel 2 no está desbloqueado.");
        }
    }

    public void JugarNivel3()
    {
        if (PlayerDataManager.Instance.playerData.level2Completed)
        {
            LoadScene("ObjetivoLvl3");
        }
        else
        {
            Debug.LogWarning("El nivel 3 no está desbloqueado.");
        }
    }

    public void Volver()
    {
        LoadScene("MainMenu");
    }

    private void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName); // Cargar la escena
    }
}
