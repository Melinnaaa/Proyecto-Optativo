using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ModifyText : MonoBehaviour, IModifyText
{
    public TextMeshProUGUI preguntaTexto;
    public TextMeshProUGUI[] alternativasTextos;
    private List<int> indicesDisponibles;
    private float tiempoInicioPregunta;
    private string[] preguntas = new string[]
    {
        "¿Cuál es el caso base de la función factorial?",
        "¿Cuál es el caso base de la función potencia?",
        "¿Cuál es el caso base de la función sumaNaturales?",
        "¿Qué estructura de datos se utiliza en la recursión para almacenar el estado de cada llamada?",
        "¿Cómo se llama el proceso de resolver las llamadas recursivas desde la última a la primera?",
        "¿Cómo se llama la parte de una función recursiva que detiene la recursión?",
        "¿Qué ocurre si una función recursiva no tiene un caso base bien definido?",
        "¿Qué técnica optimiza ciertas funciones recursivas al eliminar llamadas innecesarias?",
        "¿Cuál es una ventaja de utilizar recursión en programación?",
        "¿Qué problema es adecuado para resolver con recursión?",
        "¿Cómo se denomina a una función recursiva que se llama a sí misma varias veces en cada nivel?",
        "¿Qué es la recursión indirecta?",
        "¿Cuál es el riesgo principal de usar recursión en funciones con muchas llamadas?",
        "¿Cuál de los siguientes no es un caso base apropiado para una función que calcula el Fibonacci?",
        "¿Qué concepto es fundamental para evitar recursiones infinitas en una función recursiva?",
        "¿Qué sucede durante el proceso de desapilamiento en una función recursiva?",
        "¿Qué es la recursión de cola?",
        "¿Cuál es el caso base en una función que invierte una cadena?",
        "¿Qué tipo de recursión se produce cuando una función se llama a sí misma directamente?",
        "¿Qué estructura de control es análoga a la recursión?"
    };

    private string[][] alternativas = new string[][]
    {
        new string[] { "n = 0", "n < 0", "n <= 1", "n = 2", "n = 1", "Sin caso base" },
        new string[] { "exp = 1", "exp = 2", "exp <= 0", "exp = 0", "base = 0", "base = 1" },
        new string[] { "n = 0", "n = 1", "n < 0", "n > 1", "n = 2", "Sin caso base" },
        new string[] { "Lista", "Cola", "Pila (Stack)", "Árbol", "Conjunto", "Grafo" },
        new string[] { "Desapilamiento", "Apilamiento", "Búsqueda", "Inserción", "Ordenamiento", "Recorrido" },
        new string[] { "Llamada recursiva", "Caso base", "Bucle infinito", "Subrutina", "Punto de control", "Retorno" },
        new string[] { "Una vez", "Error compilación", "Bucle infinito", "Devuelve nulo", "Optimización", "Iterativa" },
        new string[] { "Recursión de cola", "Memoización", "Iteración", "Punteros", "Bucle for", "Variables globales" },
        new string[] { "Simplifica código", "Reduce memoria", "Aumenta velocidad", "Sin caso base", "Eliminar vars", "Legibilidad baja" },
        new string[] { "Traversal árboles", "Búsqueda en array", "Bubble Sort", "Promedio cálculo", "I/O", "Gestión memoria" },
        new string[] { "Recursión múltiple", "Recursión de cola", "Recursión directa", "Recursión indirecta", "Recursión lineal", "Recursión simple" },
        new string[] { "Función auto-llamada", "Función mutuamente", "Varios casos base", "Recursión sin cond", "Llama no recursiva", "Función anónima" },
        new string[] { "Mejor rendimiento", "Menos CPU", "Desbordamiento pila", "Código reducido", "Más legible", "Depuración fácil" },
        new string[] { "n = 0", "n = 1", "n < 0", "n = 2", "n = -1", "n = 10" },
        new string[] { "Variables globales", "Bucle for", "Caso base", "Operadores lógicos", "Punteros", "Sobrecarga" },
        new string[] { "Más llamadas", "Resolver llamadas", "Bucle infinito", "Primera llamada", "Libera memoria", "Incrementa contadores" },
        new string[] { "Recursión al inicio", "Recursión al final", "Sin caso base", "Usa bucles", "Vars globales", "Lento" },
        new string[] { "Cadena vacía", "Cadena longitud 1", "Cadena nula", "Cadena longitud 2", "Sin caso base", "Cadena con esp" },
        new string[] { "Recursión directa", "Recursión indirecta", "Recursión múltiple", "Recursión lineal", "Recursión cruzada", "Recursión interna" },
        new string[] { "Bucle for", "Bucle while", "Switch case", "Sentencia if", "Go-to", "Bucle do-while" }
    };

    private string[] respuestasCorrectas = new string[]
    {
        "n = 1",
        "exp = 0",
        "n = 0",
        "Pila (Stack)",
        "Desapilamiento",
        "Caso base",
        "Bucle infinito",
        "Recursión de cola",
        "Simplifica código",
        "Traversal árboles",
        "Recursión múltiple",
        "Función mutuamente",
        "Desbordamiento pila",
        "n = 10",
        "Caso base",
        "Resolver llamadas",
        "Recursión al final",
        "Cadena vacía",
        "Recursión directa",
        "Bucle while"
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
        StartCoroutine(CongelarPantallaPor3Segundos()); // Iniciar la pausa antes de la primera pregunta
    }

    private IEnumerator CongelarPantallaPor3Segundos()
    {
        Time.timeScale = 0; // Congela el tiempo en el juego
        yield return new WaitForSecondsRealtime(3); // Espera 3 segundos en tiempo real
        Time.timeScale = 1; // Descongela el tiempo para continuar el juego
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
        tiempoInicioPregunta = Time.time;
    }

    public bool VerificarRespuesta(string respuestaSeleccionada)
    {
        Debug.Log("Respuesta seleccionada: " + respuestaSeleccionada);
        bool esCorrecta = respuestaSeleccionada == respuestasCorrectas[preguntaIndex];

        // Llama a PlayerDataManager para registrar los datos de respuesta
        PlayerDataManager.Instance.RegistrarDatosJugador(
            preguntas[preguntaIndex],
            new List<string>(alternativas[preguntaIndex]),
            respuestaSeleccionada,
            esCorrecta,
            Time.time - tiempoInicioPregunta // Calcula el tiempo de respuesta
        );

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
        StartCoroutine(CongelarPantallaPor3Segundos());
    }
}
