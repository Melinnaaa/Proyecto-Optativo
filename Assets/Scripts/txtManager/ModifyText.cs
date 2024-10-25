using System.Collections.Generic;
using UnityEngine;

public class ModifyText : MonoBehaviour, IModifyText
{
    public TextMesh preguntaTexto; // TextMesh para mostrar la pregunta
    public TextMesh[] alternativasTextos; // Array de TextMesh para mostrar las alternativas

    private List<int> indicesDisponibles; // Lista de índices de preguntas disponibles
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
        new string[] { "n = 0", "n<0", "n<=1", "n=2", "n=1", "no tiene caso base" },
        new string[] { "exp = 1", "exp = 2", "exp<=0", "exp = 0", "base = 0", "base = 1" },
        new string[] { "n = 0", "n = 1", "n<0", "n>1", "n = 2", "no tiene caso base" },
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
        // Inicializar la lista de índices disponibles
        indicesDisponibles = new List<int>();
        for (int i = 0; i < preguntas.Length; i++)
        {
            indicesDisponibles.Add(i); // Agregar todos los índices de las preguntas
        }

        CargarPreguntaAleatoria();
    }

    public void ReiniciarPreguntas()
    {
        // Reinicializar la lista de índices disponibles
        indicesDisponibles.Clear();
        for (int i = 0; i < preguntas.Length; i++)
        {
            indicesDisponibles.Add(i);
        }
    }

    public void CargarPreguntaAleatoria()
    {
        // Verificar que aún queden preguntas disponibles
        if (indicesDisponibles.Count == 0)
        {
            return;
        }

        // Restablecer el color de los bloques antes de cargar nuevas preguntas
        for (int i = 0; i < alternativasTextos.Length; i++)
        {
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                brick.ResetColor(); // Restablece el color del bloque
            }
        }

        // Seleccionar un índice aleatorio de las preguntas disponibles
        int randomIndex = Random.Range(0, indicesDisponibles.Count);
        preguntaIndex = indicesDisponibles[randomIndex];

        // Eliminar la pregunta seleccionada de la lista de preguntas disponibles
        indicesDisponibles.RemoveAt(randomIndex);

        // Asignar la pregunta
        preguntaTexto.text = preguntas[preguntaIndex];

        // Asignar las alternativas y respuestas a los bloques
        for (int i = 0; i < alternativasTextos.Length && i < alternativas[preguntaIndex].Length; i++)
        {
            string respuestaAlternativa = alternativas[preguntaIndex][i];
            alternativasTextos[i].text = respuestaAlternativa;

            // Asignar respuesta correcta o incorrecta al bloque
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                bool esCorrecto = (respuestaAlternativa == respuestasCorrectas[preguntaIndex]);
                brick.SetAnswer(respuestaAlternativa, esCorrecto);
                brick.ResetColor();
            }
        }
    }

    public bool VerificarRespuesta(string respuestaSeleccionada)
    {
        Debug.Log("Respuesta seleccionada: '" + respuestaSeleccionada + "'");
        // Verificar si la respuesta seleccionada coincide con la respuesta correcta
        bool esCorrecta = respuestaSeleccionada == respuestasCorrectas[preguntaIndex];

        // Manejar la lógica del juego
        if (esCorrecta)
        {
            // Iniciar coroutine para cargar nueva pregunta
            CargarPreguntaAleatoria();
            return true;
        }
        else
        {
            // Registrar el error
            return false;
        }
        return false;
    }
}