using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using TMPro;

public class ModifyTextLvl2 : MonoBehaviour, IModifyText
{
    public TextMeshProUGUI preguntaTexto; // TextMesh para mostrar la pregunta
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
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas cuando se calcula fibonacci(3)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "fibo(3)", 0 },
                { "fibo(2)", 1 },
                { "fibo(1)-1ra", 2 },
                { "fibo(0)", 3 },
                { "fibo(1)-2da", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular el producto de los elementos del arreglo {2, 3, 4, 5}?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "prod(arr, 4)", 0 },
                { "prod(arr, 3)", 1 },
                { "prod(arr, 2)", 2 },
                { "prod(arr, 1)", 3 },
                { "prod(arr, 0)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para calcular mcd(462, 1071)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "mcd(462, 1071)", 0 },
                { "mcd(1071, 462)", 1 },
                { "mcd(462, 147)", 2 },
                { "mcd(147, 21)", 3 },
                { "mcd(21, 0)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas en el stack para calcular potencia(3, 4)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "pot(3, 4)", 0 },
                { "pot(3, 3)", 1 },
                { "pot(3, 2)", 2 },
                { "pot(3, 1)", 3 },
                { "pot(3, 0)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para buscar el número 5 en el arreglo {1, 2, 3, 4, 5, 6, 7, 8, 9}?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "bus(arr, 9, 5)", 0 },
                { "bus(arr, 8, 5)", 1 },
                { "bus(arr, 7, 5)", 2 },
                { "bus(arr, 6, 5)", 3 },
                { "bus(arr, 5, 5)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular la suma de los dígitos de 4321?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumDig(4321)", 0 },
                { "sumDig(432)", 1 },
                { "sumDig(43)", 2 },
                { "sumDig(4)", 3 },
                { "sumDig(0)", 4 }
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
    }

    public void CargarPreguntaAleatoria()
    {
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
                    Level2Manager.Instance.preguntasCorrectas++;
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