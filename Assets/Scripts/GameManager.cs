using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }
    public AudioSource audioSource; 
    public AudioSource backgroundMusicSource;
    public AudioClip explosionSound; 
    public AudioClip failSound; 
    public AudioClip extraLifeSound;
    public AudioClip backgroundMusic;

    public int lifes { get; private set; } = 2;
    public TextMesh scoreText;
    public List<GameObject> corazones;

    private int respuestasCorrectasContador = 0;
    private const int maxLifes = 3;
    private const int respuestasParaVida = 3;

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
    }

    private void Start()
    {
        // Solo resetea el GameManager si estamos en un nivel de juego
        string sceneName = SceneManager.GetActiveScene().name;
        if (sceneName == "Level1" || sceneName == "Level2" || sceneName == "Level3")
        {
            ResetGameManager();
            PlayBackgroundMusic();
        }
    }

    public void PlayExplosionSound(float volume = 0.5f)
    {
        if (audioSource != null && explosionSound != null)
        {
            audioSource.PlayOneShot(explosionSound, volume); // Reproduce el sonido de explosión con volumen reducido
        }
    }

    public void PlayFailSound(float volume = 0.2f)
    {
        if (audioSource != null && failSound != null)
        {
            audioSource.PlayOneShot(failSound, volume); // Reproduce el sonido de fallo con volumen reducido
        }
    }

    public void PlayExtraLifeSound(float volume = 0.5f)
    {
        if (audioSource != null && extraLifeSound != null)
        {
            audioSource.PlayOneShot(extraLifeSound, volume); // Reproduce el sonido de extra vida con volumen reducido
        }
    }

    public void PlayBackgroundMusic()
    {
        if (backgroundMusicSource != null && backgroundMusic != null && !backgroundMusicSource.isPlaying)
        {
            backgroundMusicSource.clip = backgroundMusic;
            backgroundMusicSource.loop = true; // Configura el audio para que se repita
            backgroundMusicSource.Play();
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
        respuestasCorrectasContador = 0; // Reinicia el contador de respuestas correctas
        ActualizarUI(); // Actualizar la UI con los valores iniciales
    }

    public void RegistrarRespuestaCorrecta()
    {
        respuestasCorrectasContador++;
        Debug.Log("Respuesta correcta registrada. Total de respuestas correctas: " + respuestasCorrectasContador);

        if (respuestasCorrectasContador >= respuestasParaVida)
        {
            PlayExtraLifeSound();
            OtorgarVida();
            respuestasCorrectasContador = 0; // Reinicia el contador después de otorgar una vida
        }
    }

    private void OtorgarVida()
    {
        if (lifes < maxLifes)
        {
            lifes++;
            ActualizarUI();
            Debug.Log("Se otorgó una vida adicional. Vidas actuales: " + lifes);
        }
        else
        {
            Debug.Log("Límite de vidas alcanzado. No se otorga una vida adicional.");
        }
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
