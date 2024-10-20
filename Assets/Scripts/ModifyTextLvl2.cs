using System.Collections.Generic;
using UnityEngine;

public class ModifyTextLvl2 : MonoBehaviour, IModifyText
{
    public TextMesh preguntaTexto; // TextMesh para mostrar la pregunta
    public TextMesh[] alternativasTextos; // Array de TextMesh para mostrar las alternativas

    private List<int> indicesDisponibles; // Lista de índices de preguntas disponibles

    // Preguntas del nivel 2
    private string[] preguntas = new string[]
    {
        "¿Cuál es el orden de apilamiento de las llamadas recursivas cuando se calcula fibonacci(4)?",
        "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para calcular el producto de los elementos del arreglo {2, 3, 4, 5}?",
        "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para calcular mcd(48, 18)?",
        "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para calcular potencia(3, 4)?",
        "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para buscar el número 4 en el arreglo {1, 2, 3, 4, 5}?",
        "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para calcular la suma de los dígitos de 4321?"
    };

    // Alternativas del nivel 2 (con 5 respuestas por pregunta)
    private string[][] alternativas = new string[][]
    {
        new string[] { "fibonacci(4)", "fibonacci(3)", "fibonacci(2)", "fibonacci(1)", "fibonacci(0)" },
        new string[] { "productoLista(arr, 4)", "productoLista(arr, 3)", "productoLista(arr, 2)", "productoLista(arr, 1)", "productoLista(arr, 0)" },
        new string[] { "mcd(48, 18)", "mcd(18, 12)", "mcd(12, 6)", "mcd(6, 0)", "Llamada adicional" }, // Respuesta agregada
        new string[] { "potencia(3, 4)", "potencia(3, 3)", "potencia(3, 2)", "potencia(3, 1)", "potencia(3, 0)" },
        new string[] { "buscar(arr, 5, 4)", "buscar(arr, 4, 4)", "buscar(arr, 3, 4)", "Buscar adicional", "Llamada extra" }, // Respuesta agregada
        new string[] { "sumaDigitos(4321)", "sumaDigitos(432)", "sumaDigitos(43)", "sumaDigitos(4)", "sumaDigitos(0)" }
    };

    // Orden correcto de las respuestas, mapeado según las preguntas
    private int[][] ordenCorrecto = new int[][]
    {
        new int[] { 0, 1, 2, 3, 4 }, // Fibonacci
        new int[] { 0, 1, 2, 3, 4 }, // ProductoLista
        new int[] { 0, 1, 2, 3 }, // MCD
        new int[] { 0, 1, 2, 3, 4 }, // Potencia
        new int[] { 0, 1, 2 }, // Buscar
        new int[] { 0, 1, 2, 3, 4 } // SumaDigitos
    };

    private int indiceActual = 0; // Para verificar el orden correcto
    private int preguntaIndex; // Índice de la pregunta actual

    public static ModifyTextLvl2 Instance { get; private set; }

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

    void Start()
    {
        // Inicializar la lista de índices disponibles
        indicesDisponibles = new List<int>();
        for (int i = 0; i < preguntas.Length; i++)
        {
            indicesDisponibles.Add(i);
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
            Debug.Log("¡Todas las preguntas han sido respondidas!");
            return;
        }

        // Reiniciar el color de los bloques
        for (int i = 0; i < alternativasTextos.Length; i++)
        {
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                brick.ResetColor();
            }
        }

        int randomIndex = Random.Range(0, indicesDisponibles.Count);
        preguntaIndex = indicesDisponibles[randomIndex];
        indicesDisponibles.RemoveAt(randomIndex);

        preguntaTexto.text = preguntas[preguntaIndex];

        for (int i = 0; i < alternativasTextos.Length && i < alternativas[preguntaIndex].Length; i++)
        {
            alternativasTextos[i].text = alternativas[preguntaIndex][i];

            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                brick.SetAnswer(alternativas[preguntaIndex][i], true); // Todas son correctas
            }
        }

        indiceActual = 0; // Reiniciar el índice para verificar el orden
    }

    // Método para verificar si se sigue el orden correcto
    public void VerificarRespuesta(int indiceSeleccionado)
    {
        if (indiceSeleccionado == ordenCorrecto[preguntaIndex][indiceActual])
        {
            Debug.Log("Respuesta en el orden correcto!");
            indiceActual++;

            if (indiceActual >= ordenCorrecto[preguntaIndex].Length)
            {
                Debug.Log("¡Secuencia completa correctamente!");
                CargarPreguntaAleatoria(); // Cargar una nueva pregunta cuando la secuencia es correcta
            }
        }
        else
        {
            Debug.Log("Orden incorrecto!");
            // Aquí puedes dar alguna penalización o feedback negativo
        }
    }
}
