using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ModifyTextLvl2 : MonoBehaviour, IModifyText
{
    public TextMesh preguntaTexto; // TextMesh para mostrar la pregunta
    public TextMesh[] alternativasTextos; // Array de TextMesh para mostrar las alternativas
    private Dictionary<string, int> respuestaPosicionCorrecta = new Dictionary<string, int>(); // Mapa respuesta -> posición
    private List<string> respuestasJugador = new List<string>(); // Lista para almacenar respuestas dadas por el jugador
    private int preguntaIndex; // Índice de la pregunta actual
    private List<int> indicesDisponibles;
    private int indiceActual = 0;

    // Diccionario con las alternativas (respuestas por pregunta) y sus posiciones correctas
    private Dictionary<string, int> alternativasCorrectas = new Dictionary<string, int>
    {
        { "fibonacci(4)", 0 }, { "fibonacci(3)", 1 }, { "fibonacci(2)", 2 }, { "fibonacci(1)", 3 }, { "fibonacci(0)", 4 },
        { "productoLista(arr, 4)", 0 }, { "productoLista(arr, 3)", 1 }, { "productoLista(arr, 2)", 2 }, { "productoLista(arr, 1)", 3 }, { "productoLista(arr, 0)", 4 },
        { "mcd(48, 18)", 0 }, { "mcd(18, 12)", 1 }, { "mcd(12, 6)", 2 }, { "mcd(6, 0)", 3 },
        { "potencia(3, 4)", 0 }, { "potencia(3, 3)", 1 }, { "potencia(3, 2)", 2 }, { "potencia(3, 1)", 3 }, { "potencia(3, 0)", 4 },
        { "buscar(arr, 5, 4)", 0 }, { "buscar(arr, 4, 4)", 1 }, { "buscar(arr, 3, 4)", 2 },
        { "sumaDigitos(4321)", 0 }, { "sumaDigitos(432)", 1 }, { "sumaDigitos(43)", 2 }, { "sumaDigitos(4)", 3 }, { "sumaDigitos(0)", 4 }
    };

    private string[] preguntas = new string[]
    {
        "¿Cuál es el orden de apilamiento de las llamadas recursivas cuando se calcula fibonacci(4)?",
        "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular el producto de los elementos del arreglo {2, 3, 4, 5}?",
        "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para calcular mcd(48, 18)?",
        "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para calcular potencia(3, 4)?",
        "¿Cuál es el orden en que se apilan las llamadas recursivas para buscar el número 4 en el arreglo {1, 2, 3, 4, 5}?",
        "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular la suma de los dígitos de 4321?"
    };

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
        indicesDisponibles = new List<int>();
        for (int i = 0; i < preguntas.Length; i++)
        {
            indicesDisponibles.Add(i);
        }
        CargarPreguntaAleatoria();
    }

    public void CargarPreguntaAleatoria()
    {
        preguntaIndex = Random.Range(0, preguntas.Length);
        preguntaTexto.text = preguntas[preguntaIndex];

        // Limpiar la lista de respuestas del jugador
        respuestasJugador.Clear();

        // Asignar alternativas y resetear los bloques
        for (int i = 0; i < alternativasTextos.Length; i++)
        {
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                string respuesta = alternativasCorrectas.Keys.ToList()[i]; // Convertir las claves a una lista
                alternativasTextos[i].text = respuesta;
                brick.SetAnswer(respuesta, true);
                brick.ResetColor();
            }
        }
    }

    public bool VerificarOrdenRespuesta(string respuestaSeleccionada)
    {
        // Verifica si la respuesta existe en el mapa
        if (alternativasCorrectas.TryGetValue(respuestaSeleccionada, out int posicionCorrecta))
        {
            // Verificar que el índice actual coincida con la posición correcta
            if (indiceActual == posicionCorrecta)
            {
                Debug.Log("Orden correcto!");
                indiceActual++;
                respuestasJugador.Add(respuestaSeleccionada);
                return true;
            }
            else
            {
                Debug.Log("Orden incorrecto!");
                return false; // Orden incorrecto
            }
        }
        return false;
    }

   public void VerificarRespuesta(string respuestaSeleccionada)
    {
        // Verifica si la respuesta existe en el diccionario de respuestas correctas
        if (alternativasCorrectas.TryGetValue(respuestaSeleccionada, out int posicionCorrecta))
        {
            // Si la posición actual del jugador coincide con la posición correcta
            if (indiceActual == posicionCorrecta)
            {
                Debug.Log("¡Orden correcto!");
                respuestasJugador.Add(respuestaSeleccionada);
                indiceActual++;

                // Verifica si el jugador ha completado todas las respuestas en el orden correcto
                if (indiceActual >= alternativasCorrectas.Count)
                {
                    Debug.Log("¡Secuencia completa!");
                    CargarPreguntaAleatoria(); // Cargar nueva pregunta
                    respuestasJugador.Clear(); // Reiniciar las respuestas del jugador
                    indiceActual = 0; // Reiniciar el índice
                }
            }
            else
            {
                // Orden incorrecto, marcar como error
                Debug.Log("Orden incorrecto. Restando una vida...");
                GameManager.Instance.RegistrarError(); // Restar una vida
            }
        }
        else
        {
            Debug.Log("Respuesta no válida.");
        }
    }


    public void ReiniciarPreguntas()
    {
        indicesDisponibles.Clear();
        for (int i = 0; i < preguntas.Length; i++)
        {
            indicesDisponibles.Add(i);
        }
    }
}