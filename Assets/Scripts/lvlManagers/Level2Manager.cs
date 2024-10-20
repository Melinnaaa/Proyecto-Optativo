using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class Level2Manager : MonoBehaviour, ILevelManager
{
    private int preguntasCorrectas = 0;
    private const int totalPreguntasNivel2 = 5;
    public static Level2Manager Instance { get; private set; } 

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
        ModifyTextLvl2.Instance.CargarPreguntaAleatoria(); // Cargar pregunta para el nivel 2
    }

    public bool OnPreguntaCorrecta()
    {
        preguntasCorrectas++;
        GameManager.Instance.AddScore(100); // Añadir puntaje por respuesta correcta

        if (preguntasCorrectas >= totalPreguntasNivel2)
        {
            return true; // Cargar Nivel 2 cuando todas las preguntas sean correctas
        }
        else
        {
            return false;
        }
    }
}