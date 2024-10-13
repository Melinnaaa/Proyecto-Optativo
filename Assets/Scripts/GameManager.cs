using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic; // Para usar listas

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const int NUM_LEVELS = 2;

    private Paddle paddle;
    private Brick[] bricks;

    public int level { get; private set; } = 1;
    public int score { get; private set; } = 600;
    public int lifes { get; private set; } = 2;

    public TextMesh lifesText; // Referencia al TextMesh de las vidas
    public TextMesh scoreText; // Referencia al TextMesh del puntaje
    public List<GameObject> corazones; // Lista de GameObjects con SpriteRenderer

    private int preguntasCorrectas = 0; 
    private const int totalPreguntasNivel1 = 6;

    private void Awake()
    {
        if (Instance != null)
        {
            DestroyImmediate(gameObject);
        }
        else
        {
            Instance = this;
            FindSceneReferences();
        }
        ActualizarUI();
    }

    private void OnDestroy()
    {
        if (Instance == this)
        {
            Instance = null;
        }
    }

    public void ActualizarUI()
    {
        // Actualizar el texto de las vidas y el puntaje
        //lifesText.text = lifes.ToString();
        scoreText.text = "Puntaje: " + score.ToString();

        // Actualizar los corazones en función de las vidas
        for (int i = 0; i < corazones.Count; i++)
        {
            corazones[i].SetActive(i < lifes); // Desactivar los corazones según el número de vidas
        }
    }

    private void FindSceneReferences()
    {
        paddle = FindObjectOfType<Paddle>();
        bricks = FindObjectsOfType<Brick>();
    }

    private void LoadLevel(int level)
    {
        this.level = level;
        lifes = 2;
        if (level > NUM_LEVELS)
        {
            SceneManager.LoadScene("Level1");
            ActualizarUI();
            return;
        }

        SceneManager.sceneLoaded += OnLevelLoaded;
        SceneManager.LoadScene($"Level{level}");
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
        lifes = 2;
        score = 600;
        FindSceneReferences();

        // Reinicializar preguntas al cargar el nivel
        if (scene.name == "Level1" && ModifyText.Instance != null)
        {
            preguntasCorrectas = 0;
            ModifyText.Instance.ReiniciarPreguntas();
            ModifyText.Instance.CargarPreguntaAleatoria();
            ActualizarUI();
        }
    }

    private void ResetLevel()
    {
        lifes = 2;
        ActualizarUI();
        paddle.ResetPaddle();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("MainMenu");
    }

    private void NewGame()
    {
        score = 600;
        lifes = 2;
        
        if (ModifyText.Instance != null)
        {
            ModifyText.Instance.ReiniciarPreguntas();
        }

        SceneManager.LoadScene("Level1");
        ActualizarUI();
    }

    public void OnPreguntaCorrecta()
    {
        preguntasCorrectas++;

        if (SceneManager.GetActiveScene().name == "Level1" && preguntasCorrectas >= totalPreguntasNivel1)
        {
            SceneManager.LoadScene("EndLevel1");
        }
        else
        {
            ModifyText.Instance.CargarPreguntaAleatoria();
        }
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
}
