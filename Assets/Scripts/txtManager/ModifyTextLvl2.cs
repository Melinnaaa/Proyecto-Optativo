using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System.Collections;
using TMPro;
using UnityEngine.UI;

public class ModifyTextLvl2 : MonoBehaviour, IModifyText
{
    public TextMeshProUGUI preguntaTexto; // TextMesh para mostrar la pregunta
    public TextMesh[] alternativasTextos; // Array de TextMesh para mostrar las alternativas
    public TextMeshProUGUI codigoTexto; // TextMesh para mostrar el código asociado
    private List<string> respuestasJugador = new List<string>(); // Lista para almacenar respuestas dadas por el jugador
    public ParticleSystem explosionEffect; // Efecto de explosión asignado desde el Inspector

    private int preguntaIndex; // Índice de la pregunta actual
    private int indiceActual = 0;
    public Image checkmarkImage;
    private List<Vector3> posicionesOriginales = new List<Vector3>(); // Almacena las posiciones originales de los bloques
    private bool mostrandoPregunta = true; // Controla si se muestra la pregunta o el código
    private float tiempoInicioPregunta;


    private List<Pregunta> preguntasNivel2 = new List<Pregunta>
    {
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para calcular el producto de los elementos del arreglo {2, 3, 4, 5} usando la función prodLis?",
            CodigoTexto = "int prodLis(int[] arr, int n) {\n" +
                        "    if (n == 0) return 1;\n" +
                        "    return arr[n - 1] * prodLis(arr, n - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "prodLis(arr, 0)", 0 },
                { "prodLis(arr, 1)", 1 },
                { "prodLis(arr, 2)", 2 },
                { "prodLis(arr, 3)", 3 },
                { "prodLis(arr, 4)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para calcular sumaNaturales(5) usando la función sumNat?",
            CodigoTexto = "int sumNat(int n) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return n + sumNat(n - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumNat(1)", 0 },
                { "sumNat(2)", 1 },
                { "sumNat(3)", 2 },
                { "sumNat(4)", 3 },
                { "sumNat(5)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para calcular dobleSuma(5) usando la función dobleSum?",
            CodigoTexto = "int dobleSum(int n) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return 2 * n + dobleSum(n - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "dobSum(1)", 0 },
                { "dobSum(2)", 1 },
                { "dobSum(3)", 2 },
                { "dobSum(4)", 3 },
                { "dobSum(5)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para calcular sumaCuadrado(5) usando la función sumCuad?",
            CodigoTexto = "int sumCuad(int n) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return n * n + sumCuad(n - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumCuad(1)", 0 },
                { "sumCuad(2)", 1 },
                { "sumCuad(3)", 2 },
                { "sumCuad(4)", 3 },
                { "sumCuad(5)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para calcular 3^4 usando la función potencia?",
            CodigoTexto = "int potencia(int base, int exp) {\n" +
                        "    if (exp == 0) return 1;\n" +
                        "    return base * potencia(base, exp - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "pot(3, 0)", 0 },
                { "pot(3, 1)", 1 },
                { "pot(3, 2)", 2 },
                { "pot(3, 3)", 3 },
                { "pot(3, 4)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para calcular sumMltplo(5,3) usando la función sumMltplo?",
            CodigoTexto = "int sumMltplo(int n, int m) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return m + sumMltplo(n - 1, m);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumMlt(1,3)", 0 },
                { "sumMlt(2,3)", 1 },
                { "sumMlt(3,3)", 2 },
                { "sumMlt(4,3)", 3 },
                { "sumMlt(5,3)", 4 }
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
                { "sumCub(1)", 0 },
                { "sumCub(2)", 1 },
                { "sumCub(3)", 2 },
                { "sumCub(4)", 3 },
                { "sumCub(5)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular sumMuDcr(5,5) usando la función sumMuDcr?",
            CodigoTexto = "int sumMuDcr(int n, int m) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    return m + sumMuDcr(n - 1, m);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumMuDcr(1,5)", 0 },
                { "sumMuDcr(2,5)", 1 },
                { "sumMuDcr(3,5)", 2 },
                { "sumMuDcr(4,5)", 3 },
                { "sumMuDcr(5,5)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular potBaseFija(2,5) usando la función potBaseFija?",
            CodigoTexto = "int potBaseFija(int base, int exp) {\n" +
                        "    if (exp == 0) return 1;\n" +
                        "    return base * potBaseFija(base, exp - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "potBF(2,1)", 0 },
                { "potBF(2,2)", 1 },
                { "potBF(2,3)", 2 },
                { "potBF(2,4)", 3 },
                { "potBF(2,5)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de desapilamiento de las llamadas recursivas para calcular prodAlternante(5) usando la función prodAlternante?",
            CodigoTexto = "int prodAlternante(int n) {\n" +
                        "    if (n == 0) return 1;\n" +
                        "    return n * prodAlternante(n - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "prodAlt(1)", 0 },
                { "prodAlt(2)", 1 },
                { "prodAlt(3)", 2 },
                { "prodAlt(4)", 3 },
                { "prodAlt(5)", 4 }
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
                { "fact(6)", 0 },
                { "fact(5)", 1 },
                { "fact(4)", 2 },
                { "fact(3)", 3 },
                { "fact(2)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para contar cuántas veces aparece el número 2 en el arreglo {1, 2, 2, 3, 2} usando la función contarDos?",
            CodigoTexto = "int contarDos(int[] arr, int n) {\n" +
                        "    if (n <= 0) return 0;\n" +
                        "    return (arr[n - 1] == 2 ? 1 : 0) + contarDos(arr, n - 1);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "cont(arr, 5)", 0 },
                { "cont(arr, 4)", 1 },
                { "cont(arr, 3)", 2 },
                { "cont(arr, 2)", 3 },
                { "con(arr, 1)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas para encontrar el mínimo valor en el arreglo {7, 3, 8, 1, 5} usando la función busMin?",
            CodigoTexto = "int busMin(int[] arr, int n) {\n" +
                        "    if (n == 1) return arr[0];\n" +
                        "    return Mathf.Min(arr[n - 1], busMin(arr, n - 1));\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "busMin(arr, 5)", 0 },
                { "busMin(arr, 4)", 1 },
                { "busMin(arr, 3)", 2 },
                { "busMin(arr, 2)", 3 },
                { "busMin(arr, 1)", 4 }
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
                { "conVoc(\"aureo\")", 0 },
                { "conVoc(\"ureo\")", 1 },
                { "conVoc(\"reo\")", 2 },
                { "contVoc(\"eo\")", 3 },
                { "contVoc(\"o\")", 4 }
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
                { "invCad(\"fruta\")", 0 },
                { "invCad(\"frut\")", 1 },
                { "invCad(\"fru\")", 2 },
                { "invCad(\"fr\")", 3 },
                { "invCad(\"f\")", 4 }
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
        StartCoroutine(CongelarPantallaPor3Segundos()); // Iniciar la pausa antes de la primera pregunta
    }

    private IEnumerator CongelarPantallaPor3Segundos()
    {
        Time.timeScale = 0; // Congela el tiempo en el juego
        yield return new WaitForSecondsRealtime(3); // Espera 3 segundos en tiempo real
        Time.timeScale = 1; // Descongela el tiempo para continuar el juego
    }
    private void Update()
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

    public void CargarPreguntaAleatoria()
    {
        if (preguntasNivel2.Count == 0)
        {
            return; // No hay más preguntas disponibles
        }

        preguntaIndex = Random.Range(0, preguntasNivel2.Count);
        Pregunta preguntaActual = preguntasNivel2[preguntaIndex];

        preguntaTexto.text = preguntaActual.TextoPregunta;
        codigoTexto.text = preguntaActual.CodigoTexto;

        preguntaTexto.enabled = true;
        codigoTexto.enabled = false;

        respuestasJugador.Clear();
        indiceActual = 0;

        // Restaurar las posiciones originales de los bloques
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

        // Asignar alternativas y activar los bloques
        for (int i = 0; i < alternativasTextos.Length; i++)
        {
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>();
            if (brick != null)
            {
                string respuesta = alternativasDesordenadas[i];
                alternativasTextos[i].text = respuesta;
                brick.SetAnswer(respuesta, true);

                if (brick.GetComponent<Collider2D>() != null)
                {
                    brick.GetComponent<Collider2D>().enabled = true;
                }

                brick.gameObject.SetActive(true);
            }
        }
        tiempoInicioPregunta = Time.time;
    }

    public bool VerificarRespuesta(string respuestaSeleccionada)
    {
        Pregunta preguntaActual = preguntasNivel2[preguntaIndex];
        bool esCorrecta = false;

        if (preguntaActual.AlternativasConPosicion.TryGetValue(respuestaSeleccionada, out int posicionCorrecta))
        {
            if (indiceActual == posicionCorrecta)
            {
                respuestasJugador.Add(respuestaSeleccionada);
                indiceActual++;
                esCorrecta = true;

                if (indiceActual >= preguntaActual.AlternativasConPosicion.Count)
                {
                    Level2Manager.Instance.preguntasCorrectas++;
                    preguntasNivel2.RemoveAt(preguntaIndex);
                    EjecutarAnimacionDeExito();
                    GameManager.Instance.RegistrarRespuestaCorrecta();
                }
            }
        }

        PlayerDataManager.Instance.RegistrarDatosJugador(
            preguntaActual.TextoPregunta,
            preguntaIndex, // Pasamos el índice de la pregunta
            preguntaActual.AlternativasConPosicion.Keys.ToList(),
            respuestaSeleccionada,
            esCorrecta,
            Time.time - tiempoInicioPregunta // Calcula el tiempo de respuesta
        );

        return esCorrecta;
    }

    public IEnumerator MostrarAnimacionDeExito()
    {
        float duracionTemblor = 1.0f; // Duración del temblor establecida en 1 segundo
        float intensidadTemblor = 0.1f; // Intensidad del temblor
        checkmarkImage.enabled = true;
        // Aplicar el temblor a cada bloque
        foreach (TextMesh texto in alternativasTextos)
        {
            Brick brick = texto.GetComponentInParent<Brick>();
            if (brick != null)
            {
                StartCoroutine(MoverBloqueTemblor(brick.transform, duracionTemblor, intensidadTemblor));
            }
        }
        explosionEffect.Play();

        // Espera adicional antes de cargar la siguiente pregunta
        yield return new WaitForSeconds(1.3f);
        checkmarkImage.enabled = false;
        // Cargar la siguiente pregunta
        CargarPreguntaAleatoria();
        StartCoroutine(CongelarPantallaPor3Segundos()); // Iniciar la pausa antes de la primera pregunta
    }

    private IEnumerator MoverBloqueTemblor(Transform bloque, float duracion, float intensidad)
    {
        // Esperar 1 milisegundo antes de almacenar la posición inicial
        yield return new WaitForSeconds(0.00001f);

        Vector3 posicionInicial = bloque.position;
        float tiempo = 0;

        while (tiempo < duracion)
        {
            float desplazamientoX = Random.Range(-intensidad, intensidad);
            bloque.position = new Vector3(posicionInicial.x + desplazamientoX, posicionInicial.y, posicionInicial.z);

            yield return new WaitForSeconds(0.05f); // Espera breve entre cada movimiento
            tiempo += 0.05f;
        }

        // Restaurar la posición inicial al final del temblor
        bloque.position = posicionInicial;
    }


    public void EjecutarAnimacionDeExito()
    {
        StartCoroutine(MostrarAnimacionDeExito());
    }

    public void ReiniciarPreguntas()
    {
    }
}