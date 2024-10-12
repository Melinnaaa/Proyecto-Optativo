using UnityEngine;
using UnityEngine.SceneManagement;

[DefaultExecutionOrder(-1)]
public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    private const int NUM_LEVELS = 2;

    private Paddle paddle;
    private Brick[] bricks;

    private int errores = 0;

    public int level { get; private set; } = 1;
    public int score { get; private set; } = 0;
    public int lives { get; private set; } = 3;

    private void Awake()
    {
        if (Instance != null) {
            DestroyImmediate(gameObject);
        } else {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            FindSceneReferences();
        }
    }

    private void OnDestroy()
    {
        if (Instance == this) {
            Instance = null;
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
        errores = 0;
        if (level > NUM_LEVELS)
        {
            // Start over again at level 1 once you have beaten all the levels
            // You can also load a "Win" scene instead
            SceneManager.LoadScene("Level1");
            return;
        }

        SceneManager.sceneLoaded += OnLevelLoaded;
        SceneManager.LoadScene($"Level{level}");
    }

    private void OnLevelLoaded(Scene scene, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= OnLevelLoaded;
        errores = 0;
        FindSceneReferences();

        // Reinicializar preguntas al cargar el nivel
        if (scene.name == "Level1" && ModifyText.Instance != null)
        {
            ModifyText.Instance.ReiniciarPreguntas();
            ModifyText.Instance.CargarPreguntaAleatoria();
        }
    }

    private void ResetLevel()
    {
        errores = 0;
        paddle.ResetPaddle();
    }

    public void GameOver()
    {
        SceneManager.LoadScene("MainMenu"); // Cargar la escena del menú principal
    }

    private void NewGame()
    {
        score = 0;
        lives = 3;
        errores = 0;

        // Reinicializar el ModifyText para que cargue todas las preguntas
        if (ModifyText.Instance != null)
        {
            ModifyText.Instance.ReiniciarPreguntas();
        }

        SceneManager.LoadScene("Level1");
    }

    public void OnBrickHit(Brick brick)
    {
        score += brick.points;

    }

    private bool Cleared()
    {
        for (int i = 0; i < bricks.Length; i++)
        {
            if (bricks[i] != null && bricks[i].gameObject.activeInHierarchy && !bricks[i].unbreakable)
            {
                return false;
            }
        }
        return true;
    }


    public void OnPreguntaCorrecta()
    {
        ModifyText.Instance.CargarPreguntaAleatoria(); // Cargar una nueva pregunta aleatoria que no haya sido vista
    }

    public void RegistrarError()
    {
        errores++;

        if (errores >= 2)
        {
            GameOver(); // Terminar el juego si se llega a dos errores
        }
    }

}

