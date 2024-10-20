using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Level1Manager : MonoBehaviour, ILevelManager
{
    private int preguntasCorrectas = 0;
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
        ModifyText.Instance.CargarPreguntaAleatoria(); // Cargar pregunta para el nivel 1
    }

    public bool OnPreguntaCorrecta()
    {
        preguntasCorrectas++;
        Debug.Log($"Preguntas correctas: {preguntasCorrectas} / {totalPreguntasNivel1}");

        if (preguntasCorrectas >= totalPreguntasNivel1)
        {
            return true; // Cargar Nivel 2 cuando todas las preguntas sean correctas
        }
        else
        {
            return false;
        }
    }
}
