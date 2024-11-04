using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int lifes { get; private set; } = 2;
    public TextMesh scoreText;
    public List<GameObject> corazones;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject); // Evita instancias duplicadas de `GameManager`
        }
        else
        {
            Instance = this;
            // No llamamos a ActualizarUI() aquí porque se llamará en ResetGameManager()
        }
    }

    private void Start()
    {
        // Solo resetea el GameManager si estamos en un nivel de juego
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level1" || sceneName == "Level2" || sceneName == "Level3")
        {
            ResetGameManager();
        }
    }

    public void ActualizarUI()
    {
        // Actualizar el puntaje desde ScoreManager en la UI
        if (scoreText != null)
        {
            scoreText.text = "Puntaje: " + ScoreManager.Instance.GetFinalScore().ToString();
        }
        else
        {
            Debug.LogError("scoreText es null en GameManager.ActualizarUI()");
        }

        // Actualizar corazones en la UI para mostrar las vidas restantes
        if (corazones != null && corazones.Count > 0)
        {
            for (int i = 0; i < corazones.Count; i++)
            {
                if (corazones[i] != null)
                {
                    corazones[i].SetActive(i < lifes);
                }
                else
                {
                    Debug.LogError($"corazones[{i}] es null en GameManager.ActualizarUI()");
                }
            }
        }
        else
        {
            Debug.LogError("corazones es null o está vacío en GameManager.ActualizarUI()");
        }
    }

    public void ResetGameManager()
    {
        int initialScore;

        // Determinar el puntaje inicial según el nivel actual
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level1")
        {
            initialScore = 600;
        }
        else if (sceneName == "Level2" || sceneName == "Level3")
        {
            initialScore = 500;
        }
        else
        {
            // Valor por defecto si no es ninguno de los niveles especificados
            initialScore = 0;
        }

        ScoreManager.Instance.SetFinalScore(initialScore); // Establecer el puntaje inicial
        lifes = 2; // Valor inicial de vidas
        ActualizarUI(); // Actualizar la UI con los valores iniciales
    }

    public void RegistrarError()
    {
        lifes--;
        int currentScore = ScoreManager.Instance.GetFinalScore();
        ScoreManager.Instance.SetFinalScore(currentScore - 100);
        ActualizarUI();

        // Registrar el error en el nivel actual en PlayerData
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level1")
            PlayerDataManager.Instance.playerData.level1Errors++;
        else if (sceneName == "Level2")
            PlayerDataManager.Instance.playerData.level2Errors++;
        else if (sceneName == "Level3")
            PlayerDataManager.Instance.playerData.level3Errors++;

        PlayerDataManager.Instance.SaveData();

        if (lifes <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        PlayerDataManager.Instance.SaveData();
        int finalScore = ScoreManager.Instance.GetFinalScore();
        Debug.Log("Puntaje final almacenado en ScoreManager: " + finalScore);

        // Cargar la escena de fin de nivel dependiendo del nivel actual
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level1")
        {
            SceneManager.LoadScene("EndLevel1");
        }
        else if (sceneName == "Level2")
        {
            SceneManager.LoadScene("EndLevel2");
        }
        else if (sceneName == "Level3")
        {
            SceneManager.LoadScene("EndLevel3");
        }
    }

    public void LoadLevel(int level)
    {
        SceneManager.LoadScene($"Level{level}");
    }

    public void AddScore(int points)
    {
        int currentScore = ScoreManager.Instance.GetFinalScore();
        ScoreManager.Instance.SetFinalScore(currentScore + points);
        ActualizarUI();
    }

    public bool OnHitBlock(string answer)
    {
        // Determina qué LevelManager está activo según el nivel actual
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level1" && checkAnswer(Level1Manager.Instance, 1, answer))
        {
            return true;
        }
        else if (sceneName == "Level2" && checkAnswer(Level2Manager.Instance, 2, answer))
        {
            return true;
        }
        else if (sceneName == "Level3" && checkAnswer(Level3Manager.Instance, 3, answer))
        {
            return true;
        }
        return false;
    }

    public bool checkAnswer(ILevelManager manager, int lvl, string answer)
    {
        bool state = false;
        if (manager != null && manager.checkCorrectAnswer(answer))
        {
            if (manager.isLvlFinished())
            {
                CompleteLevel(lvl);
                SceneManager.LoadScene("WinLevel" + lvl);
            }
            state = true;
        }
        return state;
    }

    public void CompleteLevel(int level)
    {
        switch (level)
        {
            case 1:
                PlayerDataManager.Instance.playerData.level1Completed = true;
                break;
            case 2:
                PlayerDataManager.Instance.playerData.level2Completed = true;
                break;
            case 3:
                PlayerDataManager.Instance.playerData.level3Completed = true;
                break;
        }
        PlayerDataManager.Instance.SaveData();
    }
}
