using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using System.Linq;
using TMPro;

public class ModifyTextLvl3 : MonoBehaviour, IModifyText
{
    public TextMeshProUGUI preguntaTexto; // TextMeshPro para mostrar la pregunta
    public TextMeshProUGUI codigoTexto;
    public TextMeshProUGUI excelente;
    public TextMesh[] alternativasTextos; // Array de TextMesh para mostrar las alternativas
    public TextMesh[] pilaAdicionalBricks; // Array de TextMesh para la pila adicional de bricks
    private bool mostrandoPregunta = true; // Controla si se muestra la pregunta o el código
    private List<string> respuestasJugador = new List<string>(); // Lista para almacenar respuestas dadas por el jugador
    private int preguntaIndex; // Índice de la pregunta actual
    private int indiceActual = 0;
    private List<Vector3> posicionesOriginales = new List<Vector3>(); // Almacena las posiciones originales de los bloques
    private float tiempoInicioPregunta;

    public ParticleSystem explosionEffect;

    private List<Pregunta> preguntasNivel3 = new List<Pregunta>
    {
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular el producto de los elementos del arreglo {2, 3, 4, 5} usando la función prodLis?",
            CodigoTexto = "int prodLis(int[] arr, int index) {\n" +
                        "    if (index == arr.Length) return 1;\n" +
                        "    return arr[index] * prodLis(arr, index + 1);\n" +
                        "}",
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
                "prodLis(arr, 0)",
                "prodLis(arr, 1)",
                "prodLis(arr, 2)",
                "prodLis(arr, 3)",
                "prodLis(arr, 4)"
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumaNaturales(5) usando la función sumNat?",
            CodigoTexto = "int sumNat(int n) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return n + sumNat(n - 1);\n" +
                        "}",
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
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular dobleSuma(5) usando la función dobleSum?",
            CodigoTexto = "int dobleSum(int n) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return 2 * n + dobleSum(n - 1);\n" +
                        "}",
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
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumaCuadrado(5) usando la función sumCuad?",
            CodigoTexto = "int sumCuad(int n) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return n * n + sumCuad(n - 1);\n" +
                        "}",
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
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular 3^4 usando la función potencia?",
            CodigoTexto = "int potencia(int base, int exp) {\n" +
                        "    if (exp == 0) return 1;\n" +
                        "    return base * potencia(base, exp - 1);\n" +
                        "}",
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
                "potencia(3, 0)",
                "potencia(3, 1)",
                "potencia(3, 2)",
                "potencia(3, 3)",
                "potencia(3, 4)"
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumMltplo(5,3) usando la función sumMltplo?",
            CodigoTexto = "int sumMltplo(int n, int m) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return n * m + sumMltplo(n - 1, m);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "3", 0 },
                { "6", 1 },
                { "9", 2 },
                { "12", 3 },
                { "15", 4 }
            },
            PilaAdicionalContenido = new List<string>
            {
                "sumMltplo(1,3)",
                "sumMltplo(2,3)",
                "sumMltplo(3,3)",
                "sumMltplo(4,3)",
                "sumMltplo(5,3)"
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumCubos(5) usando la función sumCubos?",
            CodigoTexto = "int sumCubos(int n) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return n * n * n + sumCubos(n - 1);\n" +
                        "}",
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
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumMuDcr(5, 5) usando la función sumMuDcr?",
            CodigoTexto = "int sumMuDcr(int n, int m) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return n * m + sumMuDcr(n - 1, m);\n" +
                        "}",
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
                "sumMuDcr(1,5)",
                "sumMuDcr(2,5)",
                "sumMuDcr(3,5)",
                "sumMuDcr(4,5)",
                "sumMuDcr(5,5)"
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular potBaseFija(2, 5) usando la función potBaseFija?",
            CodigoTexto = "int potBaseFija(int base, int exp) {\n" +
                        "    if (exp == 0) return 1;\n" +
                        "    return base * potBaseFija(base, exp - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "2", 0 },
                { "4", 1 },
                { "8", 2 },
                { "16", 3 },
                { "32", 4 }
            },
            PilaAdicionalContenido = new List<string>
            {
                "potBaseFija(2,1)",
                "potBaseFija(2,2)",
                "potBaseFija(2,3)",
                "potBaseFija(2,4)",
                "potBaseFija(2,5)"
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular prodAlternante(5) usando la función prodAlternante?",
            CodigoTexto = "int prodAlternante(int n) {\n" +
                        "    if (n == 0) return 1;\n" +
                        "    return (n % 2 == 0 ? -n : n) * prodAlternante(n - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "1", 0 },
                { "-2", 1 },
                { "6", 2 },
                { "-24", 3 },
                { "120", 4 }
            },
            PilaAdicionalContenido = new List<string>
            {
                "prodAlt(1)",
                "prodAlt(2)",
                "prodAlt(3)",
                "prodAlt(4)",
                "prodAlt(5)"
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para calcular el factorial(6) usando la función factorial?",
            CodigoTexto = "int factorial(int n) {\n" +
                        "    if (n <= 1) return 1;\n" +
                        "    return n * factorial(n - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "2", 0 },
                { "6", 1 },
                { "24", 2 },
                { "120", 3 },
                { "720", 4 }
            },
            PilaAdicionalContenido = new List<string>
            {
                "factorial(6)",
                "factorial(5)",
                "factorial(4)",
                "factorial(3)",
                "factorial(2)"
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para contar cuántas veces aparece el número 2 en el arreglo {1, 2, 2, 3, 2} usando la función contarDos?",
            CodigoTexto = "int contarDos(int[] arr, int index) {\n" +
                        "    if (index == arr.Length) return 0;\n" +
                        "    int count = arr[index] == 2 ? 1 : 0;\n" +
                        "    return count + contarDos(arr, index + 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "0", 0 },
                { "1-first", 1 },
                { "1-second", 2 },
                { "2", 3 },
                { "3", 4 }
            },
            PilaAdicionalContenido = new List<string>
            {
                "contDos(arr, 5)",
                "contDos(arr, 4)",
                "contDos(arr, 3)",
                "contDos(arr, 2)",
                "contDos(arr, 1)"
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para contar las vocales en la palabra 'aureo' usando la función contVocales?",
            CodigoTexto = "int contVocales(string palabra) {\n" +
                        "    if (palabra.Length == 0) return 0;\n" +
                        "    int suma = \"aeiouAEIOU\".Contains(palabra[0]) ? 1 : 0;\n" +
                        "    return suma + contVocales(palabra.Substring(1));\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "1", 0 },
                { "2-first", 1 },
                { "2-second", 2 },
                { "3", 3 },
                { "4", 4 }
            },
            PilaAdicionalContenido = new List<string>
            {
                "contVocales(\"aureo\")",
                "contVocales(\"ureo\")",
                "contVocales(\"reo\")",
                "contVocales(\"eo\")",
                "contVocales(\"o\")"
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para invertir la cadena 'fruta' usando la función invCad?",
            CodigoTexto = "string invCad(string s) {\n" +
                        "    if (s.Length == 0) return s;\n" +
                        "    return s[s.Length - 1] + invCad(s.Substring(0, s.Length - 1));\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "f", 0 },
                { "rf", 1 },
                { "urf", 2 },
                { "turf", 3 },
                { "aturf", 4 }
            },
            PilaAdicionalContenido = new List<string>
            {
                "invCad(\"fruta\")",
                "invCad(\"frut\")",
                "invCad(\"fru\")",
                "invCad(\"fr\")",
                "invCad(\"f\")"
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
        excelente.enabled = false;
    }

    void Start()
    {
        foreach (var textMesh in pilaAdicionalBricks)
        {
            if (textMesh != null)
            {
                Brick pilaBrick = textMesh.GetComponentInParent<Brick>();
                if (pilaBrick != null)
                {
                    pilaBrick.isMovable = false;
                    Vector3 posicionFija = new Vector3(-15f, textMesh.transform.position.y, textMesh.transform.position.z);
                    posicionesOriginales.Add(posicionFija);
                }
            }
        }
        StartCoroutine(CongelarPantallaPor3Segundos());
    }

    void Update()
    {
        // Detectar si se presiona Enter para alternar entre la pregunta y el código
        if (Input.GetKeyDown(KeyCode.Return))
        {
            CambiarVista();
        }
    }

    private void CambiarVista()
    {
        mostrandoPregunta = !mostrandoPregunta;

        // Alternar visibilidad entre pregunta y código
        preguntaTexto.enabled = mostrandoPregunta;
        codigoTexto.enabled = !mostrandoPregunta;
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
            return;
        }

        preguntaIndex = Random.Range(0, preguntasNivel3.Count);
        Pregunta preguntaActual = preguntasNivel3[preguntaIndex];

        preguntaTexto.text = preguntaActual.TextoPregunta;
        codigoTexto.text = preguntaActual.CodigoTexto;
        codigoTexto.enabled = false;

        respuestasJugador.Clear();
        indiceActual = 0;

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

                if (indiceActual < pilaAdicionalBricks.Length)
                {
                    Brick pilaBrick = pilaAdicionalBricks[indiceActual].GetComponentInParent<Brick>();
                    if (pilaBrick != null)
                    {
                        pilaBrick.MoverFueraDePantalla();
                    }
                }

                foreach (var texto in alternativasTextos)
                {
                    Brick brick = texto.GetComponentInParent<Brick>();
                    if (brick != null && texto.text == respuestaSeleccionada)
                    {
                        brick.MoverFueraDePantalla();
                        break;
                    }
                }

                indiceActual++;

                if (indiceActual >= preguntaActual.AlternativasConPosicion.Count)
                {
                    preguntasNivel3.RemoveAt(preguntaIndex);
                    Level3Manager.Instance.preguntasCorrectas++;
                    GameManager.Instance.RegistrarRespuestaCorrecta();
                    explosionEffect.Play();
                    excelente.enabled = true;
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
        excelente.enabled = false;
        CargarPreguntaAleatoria();
        StartCoroutine(CongelarPantallaPor3Segundos());
    }

    public void ReiniciarPreguntas()
    {
        // Método para reiniciar el banco de preguntas (si es necesario)
    }
}