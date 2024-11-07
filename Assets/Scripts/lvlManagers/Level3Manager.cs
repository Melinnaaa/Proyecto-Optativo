using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Level3Manager : MonoBehaviour, ILevelManager
{
    public int preguntasCorrectas = 0;
    private const int totalPreguntasNivel3 = 5;
    public static Level3Manager Instance { get; private set; } 

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
        ModifyTextLvl3.Instance.CargarPreguntaAleatoria(); // Cargar pregunta para el nivel 3
    }

    public bool checkCorrectAnswer(string answer)
    {
        if (ModifyTextLvl3.Instance.VerificarRespuesta(answer) == true)
        {
            return true;
        }
        return false;
    }

    public bool isLvlFinished()
    {
        Debug.Log("Preguntas correctas: " + preguntasCorrectas);
        if (preguntasCorrectas > totalPreguntasNivel3)
        {
            return true; // Cargar Nivel 2 cuando todas las preguntas sean correctas
        }
        return false;
    }
}
