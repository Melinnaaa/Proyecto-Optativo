using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ModifyTextLvl3 : MonoBehaviour, IModifyText
{
    public TextMeshProUGUI preguntaTexto; // TextMesh para mostrar la pregunta
    public TextMesh[] alternativasTextos; // Array de TextMesh para mostrar las alternativas
    private List<string> respuestasJugador = new List<string>(); // Lista para almacenar respuestas dadas por el jugador
    private int preguntaIndex; // Índice de la pregunta actual
    private int indiceActual = 0;
    private List<Vector3> posicionesOriginales = new List<Vector3>(); // Almacena las posiciones originales de los bloques

    public List<Vector3> posicionesPredefinidas = new List<Vector3>
    {
        new Vector3(9.39f, 3.16f),
        new Vector3(14.94f, 0.48f),
        new Vector3(6.26f, 5.73f),
        new Vector3(1.2f, 9.2f),
        new Vector3(-6.32f, 7.21f)
    };
   


    private List<Pregunta> preguntasNivel3 = new List<Pregunta>
    {
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular el producto de los elementos del arreglo {2, 3, 4, 5} usando la función productoLista?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "1", 0 },
                { "2", 1 },
                { "6", 2 },
                { "24", 3 },
                { "120", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumaNaturales(5) usando la función sumaNaturales?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "1", 0 },
                { "3", 1 },
                { "6", 2 },
                { "10", 3 },
                { "15", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular dobleSuma(5) usando la función dobleSuma?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "2", 0 },
                { "6", 1 },
                { "12", 2 },
                { "20", 3 },
                { "30", 4 } 
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumaCuadrado(5) usando la función sumaCuadrado?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "1", 0 },
                { "5", 1 },
                { "14", 2 },
                { "30", 3 },
                { "55", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular potenciaTres(5) usando la función potenciaTres?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "3", 0 },
                { "9", 1 },
                { "27", 2 },
                { "81", 3 }, // Respuesta ficticia para completar
                { "243", 4 }  // Respuesta ficticia para completar
            }
            a
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumaMultiplos(3, 5) usando la función sumaMultiplos?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "3", 0 },
                { "9", 1 },
                { "18", 2 },
                { "30", 3 },
                { "45", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumaCubos(5) usando la función sumaCubos?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "1", 0 },
                { "9", 1 },
                { "36", 2 },
                { "100", 3 },
                { "225", 4 }
            }
        },
         new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumaMultiplosDecrecientes(5,5) usando la función sumaMultiplosDecrecientes?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "5", 0 },
                { "15", 1 },
                { "30", 2 },
                { "50", 3 },
                { "75", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular potenciaBaseFija(2,5) usando la función potenciaBaseFija?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "2", 0 },
                { "6", 1 },
                { "14", 2 },
                { "30", 3 },
                { "62", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular productoAlternante(5) usando la función productoAlternante?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "2", 0 },
                { "4", 1 },
                { "12", 2 },
                { "24", 3 },
                { "72", 4 }
            }
        }
    };


    public static ModifyTextLvl3 Instance { get; private set; }

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
        // Incrementar el contador de preguntas correctas
        Level3Manager.Instance.preguntasCorrectas++;
        
        if (preguntasNivel3.Count == 0)
        {
            // No hay más preguntas disponibles
            return;
        }

        // Seleccionar una pregunta aleatoria
        preguntaIndex = Random.Range(0, preguntasNivel3.Count);
        Pregunta preguntaActual = preguntasNivel3[preguntaIndex];

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
        Pregunta preguntaActual = preguntasNivel3[preguntaIndex];

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
                    preguntasNivel3.RemoveAt(preguntaIndex); // Eliminar la pregunta completada
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