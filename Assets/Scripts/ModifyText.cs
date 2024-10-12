using System.Collections.Generic;
using UnityEngine;

public class ModifyText : MonoBehaviour
{
    public TextMesh preguntaTexto; // TextMesh para mostrar la pregunta
    public TextMesh[] alternativasTextos; // Array de TextMesh para mostrar las alternativas

    private List<int> indicesDisponibles; // Lista de índices de preguntas disponibles
    private string[] preguntas = new string[]
    {
        "1. ¿Cuál es el caso base de la función factorial?",
        "2. ¿Cuál es el caso base de la función potencia?",
        "3. ¿Cuál es el caso base de la función sumaNaturales?",
        "4. ¿Qué estructura de datos se utiliza en la recursión para almacenar el estado de cada llamada?",
        "5. ¿Cómo se llama el proceso de resolver las llamadas recursivas desde la última a la primera?",
        "6. ¿Cómo se llama la parte de una función recursiva que detiene la recursión?"
    };

    private string[][] alternativas = new string[][]
    {
        new string[] { "n = 0", "n = 1", "n<=1", "n=2", "n<0", "no tiene caso base" },
        new string[] { "exp = 1", "exp = 2", "exp<=0", "exp = 0", "base = 0", "base = 1" },
        new string[] { "n = 1", "n = 0", "n<0", "n>1", "n = 2", "no tiene caso base" },
        new string[] { "Lista", "Pila (Stack)", "Cola", "Árbol", "Conjunto", "Grafo" },
        new string[] { "Desapilamiento", "Apilamiento", "Búsqueda", "Inserción", "Ordenamiento", "Recorrido" },
        new string[] { "Llamada recursiva", "Caso base", "Bucle infinito", "Subrutina", "Punto de control", "Retorno" }
    };

    private char[] respuestasCorrectas = new char[] { 'b', 'd', 'b', 'b', 'b', 'b' };

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
            Debug.Log("¡Todas las preguntas han sido respondidas!");
            return;
        }

        // Restablecer el color de los bloques antes de cargar nuevas preguntas
        for (int i = 0; i < alternativasTextos.Length; i++)
        {
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                brick.ResetColor(); // Llama al método que restablece el color del bloque
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
            alternativasTextos[i].text = alternativas[preguntaIndex][i];

            // Asignar respuesta correcta o incorrecta al bloque
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                bool esCorrecto = (i == (respuestasCorrectas[preguntaIndex] - 'a'));
                brick.SetAnswer(alternativas[preguntaIndex][i], esCorrecto);
            }
        }
    }


    // Método para verificar si la respuesta seleccionada es correcta
    public void VerificarRespuesta(int indiceSeleccionado)
    {
        // Convertir el índice a letra para compararlo con la respuesta correcta
        char seleccion = (char)('a' + indiceSeleccionado);

        if (seleccion == respuestasCorrectas[preguntaIndex])
        {
            Debug.Log("Respuesta Correcta!");
            CargarPreguntaAleatoria(); // Cargar una nueva pregunta si es correcta
        }
        else
        {
            Debug.Log("Respuesta Incorrecta!");
            // Aquí puedes hacer que el bloque cambie a rojo o dar feedback de error
        }
    }
}
