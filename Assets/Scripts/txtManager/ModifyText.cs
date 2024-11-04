﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifyText : MonoBehaviour, IModifyText
{
    public TextMeshProUGUI preguntaTexto;
    public TextMeshProUGUI[] alternativasTextos;

    private List<int> indicesDisponibles;
    private string[] preguntas = new string[]
    {
        "¿Cuál es el caso base de la función factorial?",
        "¿Cuál es el caso base de la función potencia?",
        "¿Cuál es el caso base de la función sumaNaturales?",
        "¿Qué estructura de datos se utiliza en la recursión para almacenar el estado de cada llamada?",
        "¿Cómo se llama el proceso de resolver las llamadas recursivas desde la última a la primera?",
        "¿Cómo se llama la parte de una función recursiva que detiene la recursión?"
    };

    private string[][] alternativas = new string[][]
    {
        new string[] { "n = 0", "n<0", "n<=1", "n=2", "n=1", "No tiene" },
        new string[] { "exp = 1", "exp = 2", "exp<=0", "exp = 0", "base = 0", "base = 1" },
        new string[] { "n = 0", "n = 1", "n<0", "n>1", "n = 2", "No tiene" },
        new string[] { "Lista", "Cola", "Pila (Stack)", "Árbol", "Conjunto", "Grafo" },
        new string[] { "Desapilamiento", "Apilamiento", "Búsqueda", "Inserción", "Ordenamiento", "Recorrido" },
        new string[] { "Llamada recursiva", "Caso base", "Bucle infinito", "Subrutina", "Punto de control", "Retorno" }
    };

    private string[] respuestasCorrectas = new string[]
    {
        "n=1",
        "exp = 0",
        "n = 0",
        "Pila (Stack)",
        "Desapilamiento",
        "Caso base"
    };

    private int preguntaIndex;

    public static ModifyText Instance { get; private set; }

    private void Awake()
    {
        if (Instance != null) 
        {
            DestroyImmediate(gameObject); // Evitar duplicados si ya existe una instancia
        } 
        else 
        {
            Instance = this;
        }
    }

    void Start()
    {
        indicesDisponibles = new List<int>();
        for (int i = 0; i < preguntas.Length; i++)
        {
            indicesDisponibles.Add(i); // Agregar todos los índices de las preguntas
        }

        CargarPreguntaAleatoria();
    }

    public void ReiniciarPreguntas()
    {
        indicesDisponibles.Clear();
        for (int i = 0; i < preguntas.Length; i++)
        {
            indicesDisponibles.Add(i);
        }
    }

    public void CargarPreguntaAleatoria()
    {
        if (indicesDisponibles.Count == 0)
        {
            Debug.Log("No hay más preguntas disponibles.");
            return;
        }

        // Restablecer color de bloques
        foreach (var textoAlternativa in alternativasTextos)
        {
            Brick brick = textoAlternativa.GetComponentInParent<Brick>();
            if (brick != null)
            {
                brick.ResetColor();
            }
        }

        // Seleccionar una pregunta aleatoria
        int randomIndex = Random.Range(0, indicesDisponibles.Count);
        preguntaIndex = indicesDisponibles[randomIndex];
        indicesDisponibles.RemoveAt(randomIndex);

        // Mostrar la pregunta
        preguntaTexto.text = preguntas[preguntaIndex];

        // Configurar alternativas
        for (int i = 0; i < alternativasTextos.Length && i < alternativas[preguntaIndex].Length; i++)
        {
            string respuestaAlternativa = alternativas[preguntaIndex][i];
            alternativasTextos[i].text = respuestaAlternativa;

            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                bool esCorrecto = respuestaAlternativa == respuestasCorrectas[preguntaIndex];
                brick.SetAnswer(respuestaAlternativa, esCorrecto);
                brick.ResetColor();
            }
        }
    }

    public bool VerificarRespuesta(string respuestaSeleccionada)
    {
        Debug.Log("Respuesta seleccionada: " + respuestaSeleccionada);
        bool esCorrecta = respuestaSeleccionada == respuestasCorrectas[preguntaIndex];

        if (esCorrecta)
        {
            Debug.Log("Respuesta correcta.");
            StartCoroutine(CargarPreguntaConRetraso());
            return true;
        }
        else
        {
            Debug.Log("Respuesta incorrecta.");
            return false;
        }
    }

    private IEnumerator CargarPreguntaConRetraso()
    {
        yield return new WaitForSeconds(0.000001f); // Espera antes de cargar la nueva pregunta
        CargarPreguntaAleatoria();
    }
}
