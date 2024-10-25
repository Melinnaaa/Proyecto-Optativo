using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public int score { get; private set; } = 600;
    public int lifes { get; private set; } = 2;

    public TextMesh scoreText; 
    public List<GameObject> corazones; 

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
        }
        ActualizarUI();
    }

    public void ActualizarUI()
    {
        // Actualizar las vidas y puntaje en la interfaz
        scoreText.text = "Puntaje: " + score.ToString();
        for (int i = 0; i < corazones.Count; i++)
        {
            corazones[i].SetActive(i < lifes);
        }
    }

    public void ResetGameManager()
    {
        score = 600; // Valor inicial del puntaje
        lifes = 2; // Reinicia las vidas
        ActualizarUI(); // Actualiza la interfaz con los valores iniciales
    }
    
    public void RegistrarError()
    {
        lifes--;
        score -= 100;
        ActualizarUI();
        if (lifes <= 0)
        {
            GameOver();
        }
    }

    public void GameOver()
    {
        GameData.finalScore = score;  
        Debug.Log("Puntaje almacenado en GameData: " + GameData.finalScore);
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            SceneManager.LoadScene("EndLevel1");
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            SceneManager.LoadScene("EndLevel2");
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            SceneManager.LoadScene("EndLevel3");
        }
    }

    public void LoadLevel(int level)
    {
        ResetGameManager();
        SceneManager.LoadScene($"Level{level}");
    }

    public void AddScore(int points)
    {
        score += points;
        ActualizarUI();
    }

    public bool OnHitBlock(string answer)
    {
        // Determina qué LevelManager está activo según el nivel
        if (SceneManager.GetActiveScene().name == "Level1")
        {
            if (checkAnswer(Level1Manager.Instance, ModifyText.Instance, 1, answer))
            {
                return true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level2")
        {
            if (checkAnswer(Level2Manager.Instance, ModifyTextLvl2.Instance, 2, answer))
            {
                return true;
            }
        }
        else if (SceneManager.GetActiveScene().name == "Level3")
        {
            if (checkAnswer(Level2Manager.Instance, ModifyTextLvl3.Instance, 2, answer))
            {
                return true;
            }
        }
        return false;
    }

    public bool checkAnswer(object lvlManager, object text, int lvl, string answer)
    {
        bool state = false;
        if (lvlManager is ILevelManager manager)
        {   
            if (manager.checkCorrectAnswer(answer) == true)
            {
                if (manager.isLvlFinished() == true)
                {
                    GameData.finalScore = score;
                    SceneManager.LoadScene("WinLevel" + lvl);
                }
                state = true;
            }
        }
        return state;
    }

}
