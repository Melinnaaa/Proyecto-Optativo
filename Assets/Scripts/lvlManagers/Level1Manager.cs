using UnityEngine;

public class Level1Manager : MonoBehaviour, ILevelManager
{
    public int preguntasCorrectas = 0;
    private const int totalPreguntasNivel1 = 6;
    public static Level1Manager Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }

    void Start()
    {
    }

    public bool checkCorrectAnswer(string answer)
    {
        bool resultado = ModifyText.Instance.VerificarRespuesta(answer);
        if (resultado)
        {
            preguntasCorrectas++;
        }
        return resultado;
    }

    public bool isLvlFinished()
    {
        return preguntasCorrectas >= totalPreguntasNivel1;
    }
}
