using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;

public class ModifyTextLvl2 : MonoBehaviour, IModifyText
{
    public TextMesh preguntaTexto; // TextMesh para mostrar la pregunta
    public TextMesh[] alternativasTextos; // Array de TextMesh para mostrar las alternativas
    private List<string> respuestasJugador = new List<string>(); // Lista para almacenar respuestas dadas por el jugador
    private int preguntaIndex; // Índice de la pregunta actual
    private int indiceActual = 0;
    private List<Vector3> posicionesOriginales = new List<Vector3>(); // Almacena las posiciones originales de los bloques

    public List<Vector2> posicionesPredefinidas = new List<Vector2>
    {
        new Vector2(9.39f, 3.16f),
        new Vector2(14.94f, 0.48f),
        new Vector2(6.26f, 5.73f),
        new Vector2(1.2f, 9.2f),
        new Vector2(-6.32f, 7.21f)
    };

    private List<Pregunta> preguntasNivel2 = new List<Pregunta>
    {
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas cuando se calcula fibonacci(4)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "fibonacci(4)", 0 },
                { "fibonacci(3)", 1 },
                { "fibonacci(2)", 2 },
                { "fibonacci(1)", 3 },
                { "fibonacci(0)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular el producto de los elementos del arreglo {2, 3, 4, 5}?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "productoLista(arr, 4)", 0 },
                { "productoLista(arr, 3)", 1 },
                { "productoLista(arr, 2)", 2 },
                { "productoLista(arr, 1)", 3 },
                { "productoLista(arr, 0)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para calcular mcd(48, 18)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "mcd(48, 18)", 0 },
                { "mcd(18, 12)", 1 },
                { "mcd(12, 6)", 2 },
                { "mcd(6, 0)", 3 },
                { "Respuesta ficticia 1", 4 } // Respuesta ficticia para completar
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para calcular potencia(3, 4)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "potencia(3, 4)", 0 },
                { "potencia(3, 3)", 1 },
                { "potencia(3, 2)", 2 },
                { "potencia(3, 1)", 3 },
                { "potencia(3, 0)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para buscar el número 4 en el arreglo {1, 2, 3, 4, 5}?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "buscar(arr, 5, 4)", 0 },
                { "buscar(arr, 4, 4)", 1 },
                { "buscar(arr, 3, 4)", 2 },
                { "Respuesta ficticia 1", 3 }, // Respuesta ficticia para completar
                { "Respuesta ficticia 2", 4 }  // Respuesta ficticia para completar
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular la suma de los dígitos de 4321?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumaDigitos(4321)", 0 },
                { "sumaDigitos(432)", 1 },
                { "sumaDigitos(43)", 2 },
                { "sumaDigitos(4)", 3 },
                { "sumaDigitos(0)", 4 }
            }
        }
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
        // Guardar las posiciones originales de los bloques en el inicio del juego
        for (int i = 0; i < alternativasTextos.Length; i++)
        {
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                posicionesOriginales.Add(brick.transform.position); // Guardar la posición original
            }
        }

        CargarPreguntaAleatoria();
    }

    public void CargarPreguntaAleatoria()
    {
        // Incrementar el contador de preguntas correctas
        Level2Manager.Instance.preguntasCorrectas++;
        
        if (preguntasNivel2.Count == 0)
        {
            // No hay más preguntas disponibles
            return;
        }

        // Seleccionar una pregunta aleatoria
        preguntaIndex = Random.Range(0, preguntasNivel2.Count);
        Pregunta preguntaActual = preguntasNivel2[preguntaIndex];

        // Mostrar el texto de la pregunta
        preguntaTexto.text = preguntaActual.TextoPregunta;

        // Reiniciar variables
        respuestasJugador.Clear();
        indiceActual = 0;

        // Restaurar las posiciones originales de los bloques antes de cualquier asignación de nuevas posiciones
        for (int i = 0; i < alternativasTextos.Length; i++)
        {
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            Brick.stackPosition = new Vector3(-15, -8, 0);
            if (brick != null)
            {
                brick.ResetBrick();
            }
        }

         // Mezclar las alternativas y asignarlas a los bloques
        List<string> alternativasDesordenadas = preguntaActual.AlternativasConPosicion.Keys.ToList();
        alternativasDesordenadas = alternativasDesordenadas.OrderBy(a => Random.value).ToList();

        // Asegurarse de que cada bloque esté activo y asignar las nuevas alternativas
        for (int i = 0; i < alternativasTextos.Length; i++)
        {
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                // Asignar la alternativa y la respuesta correcta/incorrecta
                string respuesta = alternativasDesordenadas[i];
                alternativasTextos[i].text = respuesta;
                brick.SetAnswer(respuesta, true);

                // Asegurarse de que el collider esté habilitado
                if (brick.GetComponent<Collider2D>() != null)
                {
                    brick.GetComponent<Collider2D>().enabled = true;
                }

                // Mantener el bloque activo
                brick.gameObject.SetActive(true);
            }
        }
    }

    public bool VerificarRespuesta(string respuestaSeleccionada)
    {
        Pregunta preguntaActual = preguntasNivel2[preguntaIndex];

        // Verificar si la respuesta seleccionada existe en las alternativas de la pregunta actual
        if (preguntaActual.AlternativasConPosicion.TryGetValue(respuestaSeleccionada, out int posicionCorrecta))
        {
            // Verificar si la posición correcta coincide con el índice actual
            if (indiceActual == posicionCorrecta)
            {
                respuestasJugador.Add(respuestaSeleccionada);
                indiceActual++;

                // Verificar si se completó la secuencia
                if (indiceActual >= preguntaActual.AlternativasConPosicion.Count)
                {
                    preguntasNivel2.RemoveAt(preguntaIndex); // Eliminar la pregunta completada
                    StartCoroutine(CargarPreguntaConRetraso());
                }

                return true;
            }
            else
            {
                return false;
            }
        }
        else
        {
            return false;
        }
    }

    private IEnumerator CargarPreguntaConRetraso()
    {
        // Esperar 1 segundo antes de cargar la pregunta
        yield return new WaitForSeconds(0.00001f);

        CargarPreguntaAleatoria(); // Cargar la nueva pregunta
    }

    public void ReiniciarPreguntas()
    {
    }
}