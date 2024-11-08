using System.Collections.Generic;
using System.Collections;
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
    private float tiempoInicioPregunta;


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
                "prodLis(1)",
                "prodLis(2)",
                "prodLis(3)",
                "prodLis(4)",
                "prodLis(5)"
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
                "sumNat(1)",
                "sumNat(2)",
                "sumNat(3)",
                "sumNat(4)",
                "sumNat(5)"
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
                "dobleSum(1)",
                "dobleSum(2)",
                "dobleSum(3)",
                "dobleSum(4)",
                "dobleSum(5)"
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
                "sumCuad(1)",
                "sumCuad(2)",
                "sumCuad(3)",
                "sumCuad(4)",
                "sumCuad(5)"
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
                "potTres(1)",
                "potTres(2)",
                "potTres(3)",
                "potTres(4)",
                "potTres(5)"
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
                "sumMult(1)",
                "sumMult(2)",
                "sumMult(3)",
                "sumMult(4)",
                "sumMult(5)"
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
                "sumCubos(1)",
                "sumCubos(2)",
                "sumCubos(3)",
                "sumCubos(4)",
                "sumCubos(5)"
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
                "sumMultDec(1)",
                "sumMultDec(2)",
                "sumMultDec(3)",
                "sumMultDec(4)",
                "sumMultDec(5)"
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
                "potBaseF(1)",
                "potBaseF(2)",
                "potBaseF(3)",
                "potBaseF(4)",
                "potBaseF(5)"
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
                "prodAlt(1)",
                "prodAlt(2)",
                "prodAlt(3)",
                "prodAlt(4)",
                "prodAlt(5)"
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
        foreach (var textMesh in pilaAdicionalBricks)
        {
            if (textMesh != null)
            {
                posicionesOriginales.Add(textMesh.transform.position);
                
                Brick pilaBrick = textMesh.GetComponentInParent<Brick>();
                if (pilaBrick != null)
                {
                    pilaBrick.isMovable = false;  // Configurar para que estos bricks nunca se muevan
                }
            }
        }
    }

    private IEnumerator CongelarPantallaPor3Segundos()
    {
        Time.timeScale = 0;
        yield return new WaitForSecondsRealtime(3);
        Time.timeScale = 1;
    }

    public void CargarPreguntaAleatoria()
    {
        if (preguntasNivel3.Count == 0)
        {
            Debug.Log("No hay más preguntas disponibles.");
            return; // No hay más preguntas disponibles
        }

        preguntaIndex = Random.Range(0, preguntasNivel3.Count);
        Pregunta preguntaActual = preguntasNivel3[preguntaIndex];

        preguntaTexto.text = preguntaActual.TextoPregunta;
        respuestasJugador.Clear();
        indiceActual = 0;

        // Restaurar las posiciones originales de la pila adicional
        for (int i = 0; i < pilaAdicionalBricks.Length; i++)
        {
            if (pilaAdicionalBricks[i] != null && i < posicionesOriginales.Count)
            {
                Brick brick = pilaAdicionalBricks[i].GetComponentInParent<Brick>();
                if (brick != null)
                {
                    brick.transform.position = posicionesOriginales[i];
                    brick.isMovable = false; // Asegurarse de que los bloques de la pila no se muevan
                    pilaAdicionalBricks[i].text = preguntaActual.PilaAdicionalContenido[i];
                }
            }
        }

        // Configuración para cargar la nueva pregunta en alternativasTextos
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
                brick.ResetBrick();
            }
        }

        tiempoInicioPregunta = Time.time;
    }

    public bool VerificarRespuesta(string respuestaSeleccionada)
    {
        Pregunta preguntaActual = preguntasNivel3[preguntaIndex];
        bool esCorrecta = false;

        if (preguntaActual.AlternativasConPosicion.TryGetValue(respuestaSeleccionada, out int posicionCorrecta))
        {
            if (indiceActual == posicionCorrecta)
            {
                respuestasJugador.Add(respuestaSeleccionada);
                esCorrecta = true;

                // Mover el primer bloque de la pila adicional fuera de la pantalla
                if (indiceActual < pilaAdicionalBricks.Length)
                {
                    Brick pilaBrick = pilaAdicionalBricks[indiceActual].GetComponentInParent<Brick>();
                    if (pilaBrick != null)
                    {
                        pilaBrick.MoverFueraDePantalla();
                        Debug.Log($"Moviendo fuera de pantalla el bloque de la pila: {pilaBrick.gameObject.name}");
                    }
                }

                // Mover el bloque de alternativas al que se disparó
                foreach (var texto in alternativasTextos)
                {
                    Brick brick = texto.GetComponentInParent<Brick>();
                    if (brick != null && texto.text == respuestaSeleccionada)
                    {
                        brick.MoverFueraDePantalla();
                        Debug.Log($"Moviendo fuera de pantalla el bloque de alternativas: {brick.gameObject.name}");
                        break;
                    }
                }

                indiceActual++;

                if (indiceActual >= preguntaActual.AlternativasConPosicion.Count)
                {
                    preguntasNivel3.RemoveAt(preguntaIndex);
                    Level3Manager.Instance.preguntasCorrectas++;
                    GameManager.Instance.RegistrarRespuestaCorrecta();
                    StartCoroutine(CargarPreguntaConRetraso());
                }
            }
        }

        PlayerDataManager.Instance.RegistrarDatosJugador(
            preguntaActual.TextoPregunta,
            preguntaActual.AlternativasConPosicion.Keys.ToList(),
            respuestaSeleccionada,
            esCorrecta,
            Time.time - tiempoInicioPregunta
        );

        return esCorrecta;
    }

    private IEnumerator CargarPreguntaConRetraso()
    {
        yield return new WaitForSeconds(1.0f);
        CargarPreguntaAleatoria();
        StartCoroutine(CongelarPantallaPor3Segundos());
    }

    public void ReiniciarPreguntas()
    {
        // Método para reiniciar el banco de preguntas (si es necesario)
    }
}