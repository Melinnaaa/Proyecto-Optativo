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
            TextoPregunta = "¿Cuál es el orden de apilamiento de las llamadas recursivas cuando se calcula fibo(3)?",
            CodigoTexto = "int fibo(int n) {\n" +
                        "    if (n <= 1) return n;\n" +
                        "    else return fibo(n - 1) + fibo(n - 2);\n" +
                        "}",
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
            CodigoTexto = "int prod(int arr[], int n) {\n" +
                        "    if (n == 0) return 1;\n" +
                        "    else return arr[n - 1] * prod(arr, n - 1);\n" +
                        "}",
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
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular mcd(462, 1071)?",
            CodigoTexto = "int mcd(int a, int b) {\n" +
                        "    if (b == 0) return a;\n" +
                        "    else return mcd(b, a % b);\n" +
                        "}",
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
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular pot(3, 4)?",
            CodigoTexto = "int pot(int base, int exp) {\n" +
                        "    if (exp == 0) return 1;\n" +
                        "    else return base * pot(base, exp - 1);\n" +
                        "}",
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
            CodigoTexto = "int bus(int arr[], int n, int target) {\n" +
                        "    if (arr[n] == target) return n;\n" +
                        "    else return buscar(arr, n - 1, target);\n" +
                        "}",
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
            CodigoTexto = "int sumaDigitos(int n) {\n" +
                        "    if (n == 0) return 0;\n" +
                        "    else return (n % 10) + sumaDigitos(n / 10);\n" +
                        "}",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumaDig(4321)", 0 },
                { "sumaDig(432)", 1 },
                { "sumaDig(43)", 2 },
                { "sumaDig(4)", 3 },
                { "sumaDig(0)", 4 }
            }
        },
        // Preguntas adicionales de nivel 3 en orden de apilamiento
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular sumaNaturales(5)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumNat(5)", 0 },
                { "sumNat(4)", 1 },
                { "sumNat(3)", 2 },
                { "sumNat(2)", 3 },
                { "sumNat(1)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular dobleSuma(5)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "dobleSum(5)", 0 },
                { "dobleSum(4)", 1 },
                { "dobleSum(3)", 2 },
                { "dobleSum(2)", 3 },
                { "dobleSum(1)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular sumaCuadrado(5)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumCuad(5)", 0 },
                { "sumCuad(4)", 1 },
                { "sumCuad(3)", 2 },
                { "sumCuad(2)", 3 },
                { "sumCuad(1)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular potenciaTres(5)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "potTres(5)", 0 },
                { "potTres(4)", 1 },
                { "potTres(3)", 2 },
                { "potTres(2)", 3 },
                { "potTres(1)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular sumaMultiplos(3, 5)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumMult(5)", 0 },
                { "sumMult(4)", 1 },
                { "sumMult(3)", 2 },
                { "sumMult(2)", 3 },
                { "sumMult(1)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular sumaCubos(5)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumCubos(5)", 0 },
                { "sumCubos(4)", 1 },
                { "sumCubos(3)", 2 },
                { "sumCubos(2)", 3 },
                { "sumCubos(1)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular sumaMultiplosDecrecientes(5,5)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "sumMultDec(5)", 0 },
                { "sumMultDec(4)", 1 },
                { "sumMultDec(3)", 2 },
                { "sumMultDec(2)", 3 },
                { "sumMultDec(1)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular potenciaBaseFija(2,5)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "potBaseF(5)", 0 },
                { "potBaseF(4)", 1 },
                { "potBaseF(3)", 2 },
                { "potBaseF(2)", 3 },
                { "potBaseF(1)", 4 }
            }
        },
        new Pregunta
        {
            TextoPregunta = "¿Cuál es el orden en que se apilan las llamadas recursivas para calcular productoAlternante(5)?",
            AlternativasConPosicion = new Dictionary<string, int>
            {
                { "prodAlt(5)", 0 },
                { "prodAlt(4)", 1 },
                { "prodAlt(3)", 2 },
                { "prodAlt(2)", 3 },
                { "prodAlt(1)", 4 }
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
                }
            }
        }

        // Registrar los datos de respuesta en PlayerDataManager
        PlayerDataManager.Instance.RegistrarDatosJugador(
            preguntaActual.TextoPregunta,
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