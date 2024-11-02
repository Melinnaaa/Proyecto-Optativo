using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using TMPro;

public class ModifyTextLvl3 : MonoBehaviour, IModifyText
{
    public TextMeshProUGUI preguntaTexto; // TextMeshPro para mostrar la pregunta
    public TextMesh[] alternativasTextos; // Array de TextMesh para mostrar las alternativas
    public TextMesh[] pilaAdicionalBricks; // Array de TextMesh para la pila adicional de bricks

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
            },
            PilaAdicionalContenido = new List<string>
            {
                "productoLista(1)",
                "productoLista(2)",
                "productoLista(3)",
                "productoLista(4)",
                "productoLista(5)"
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
            },
            PilaAdicionalContenido = new List<string>
            {
                "sumaNaturales(1)",
                "sumaNaturales(2)",
                "sumaNaturales(3)",
                "sumaNaturales(4)",
                "sumaNaturales(5)"
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
            },
            PilaAdicionalContenido = new List<string>
            {
                "dobleSuma(1)",
                "dobleSuma(2)",
                "dobleSuma(3)",
                "dobleSuma(4)",
                "dobleSuma(5)"
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
            },
            PilaAdicionalContenido = new List<string>
            {
                "sumaCuadrado(1)",
                "sumaCuadrado(2)",
                "sumaCuadrado(3)",
                "sumaCuadrado(4)",
                "sumaCuadrado(5)"
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
                { "81", 3 },
                { "243", 4 }
            },
            PilaAdicionalContenido = new List<string>
            {
                "potenciaTres(1)",
                "potenciaTres(2)",
                "potenciaTres(3)",
                "potenciaTres(4)",
                "potenciaTres(5)"
            }
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
            },
            PilaAdicionalContenido = new List<string>
            {
                "sumaMultiplos(1)",
                "sumaMultiplos(2)",
                "sumaMultiplos(3)",
                "sumaMultiplos(4)",
                "sumaMultiplos(5)"
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
            },
            PilaAdicionalContenido = new List<string>
            {
                "sumaCubos(1)",
                "sumaCubos(2)",
                "sumaCubos(3)",
                "sumaCubos(4)",
                "sumaCubos(5)"
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
            },
            PilaAdicionalContenido = new List<string>
            {
                "sumaMultiplosDecrecientes(1)",
                "sumaMultiplosDecrecientes(2)",
                "sumaMultiplosDecrecientes(3)",
                "sumaMultiplosDecrecientes(4)",
                "sumaMultiplosDecrecientes(5)"
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
            },
            PilaAdicionalContenido = new List<string>
            {
                "potenciaBaseFija(1)",
                "potenciaBaseFija(2)",
                "potenciaBaseFija(3)",
                "potenciaBaseFija(4)",
                "potenciaBaseFija(5)"
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
            },
            PilaAdicionalContenido = new List<string>
            {
                "productoAlternante(1)",
                "productoAlternante(2)",
                "productoAlternante(3)",
                "productoAlternante(4)",
                "productoAlternante(5)"
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

        CargarPreguntaAleatoria();
    }

    public void CargarPreguntaAleatoria()
    {
        if (preguntasNivel3.Count == 0)
        {
            return; // No hay más preguntas disponibles
        }

        preguntaIndex = Random.Range(0, preguntasNivel3.Count);
        Pregunta preguntaActual = preguntasNivel3[preguntaIndex];

        preguntaTexto.text = preguntaActual.TextoPregunta;
        respuestasJugador.Clear();
        indiceActual = 0;

        for (int i = 0; i < alternativasTextos.Length; i++)
        {
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                brick.ResetBrick();
                brick.transform.position = posicionesOriginales[i];
            }
        }

        List<string> alternativasDesordenadas = preguntaActual.AlternativasConPosicion.Keys.ToList();
        alternativasDesordenadas = alternativasDesordenadas.OrderBy(a => Random.value).ToList();

        for (int i = 0; i < alternativasTextos.Length; i++)
        {
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                string respuesta = alternativasDesordenadas[i];
                alternativasTextos[i].text = respuesta;
                brick.SetAnswer(respuesta, true);
                brick.gameObject.SetActive(true);
            }
        }

        MostrarPilaAdicional();
    }

    public void MostrarPilaAdicional()
    {
        Pregunta preguntaActual = preguntasNivel3[preguntaIndex];

        // Asignar el contenido de PilaAdicionalContenido a los bricks en pilaAdicionalBricks
        for (int i = 0; i < pilaAdicionalBricks.Length; i++)
        {
            if (i < preguntaActual.PilaAdicionalContenido.Count)
            {
                // Asigna el texto correspondiente a cada brick en pilaAdicionalBricks
                pilaAdicionalBricks[i].text = preguntaActual.PilaAdicionalContenido[i];
            }
            else
            {
                // Si hay menos contenido que bricks, limpia los textos sobrantes
                pilaAdicionalBricks[i].text = ""; 
            }
        }
    }
   

    public bool VerificarRespuesta(string respuestaSeleccionada)
    {
        Pregunta preguntaActual = preguntasNivel3[preguntaIndex];

        if (preguntaActual.AlternativasConPosicion.TryGetValue(respuestaSeleccionada, out int posicionCorrecta))
        {
            if (indiceActual == posicionCorrecta)
            {
                respuestasJugador.Add(respuestaSeleccionada);
                indiceActual++;

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

    private System.Collections.IEnumerator CargarPreguntaConRetraso()
{
    yield return new WaitForSeconds(1.0f);
    CargarPreguntaAleatoria(); // Cargar la nueva pregunta
}


    public void ReiniciarPreguntas()
    {
        // Método para reiniciar el banco de preguntas (si es necesario)
    }
    
}
