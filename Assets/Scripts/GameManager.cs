using UnityEngine;
using UnityEngine.SceneManagement;

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

    public TextMesh lifesText; // Referencia al TextMesh de las lifesText
    public TextMesh scoreText; // Referencia al TextMesh del puntaje

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
        if (Instance == this) {
            Instance = null;
        }
    }

    public void ActualizarUI()
    {
        lifesText.text = "Vidas: " + lifes.ToString();
        scoreText.text = "Puntaje: " + score.ToString();
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
            // Start over again at level 1 once you have beaten all the levels
            // You can also load a "Win" scene instead
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
        SceneManager.LoadScene("MainMenu"); // Cargar la escena del menú principal
    }

    private void NewGame()
    {
        score = 600;
        lifes = 2;
        
        // Reinicializar el ModifyText para que cargue todas las preguntas
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
            // El jugador ha respondido correctamente todas las preguntas del Nivel 1
            SceneManager.LoadScene("EndLevel1");
        }
        else
        {
            ModifyText.Instance.CargarPreguntaAleatoria(); // Cargar una nueva pregunta
        }
    }

    public void RegistrarError()
    {
        lifes--;
        score = score - 100;
        ActualizarUI();
        if (lifes <= 0)
        {
            GameOver(); // Terminar el juego si se llega a dos lifesText
        }
    }

}

