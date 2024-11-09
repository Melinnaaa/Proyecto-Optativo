using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadTips : MonoBehaviour
{
    private string[] tipsNivel1 = new string[]
    {
        // Pregunta 1
        "En el factorial, el caso base es el valor de 'n' que detiene las multiplicaciones sucesivas.",
        
        // Pregunta 2
        "En la función potencia, considera el exponente que hace que cualquier base elevada a él sea 1.",
        
        // Pregunta 3
        "En sumaNaturales, piensa en el valor de 'n' que hace que la suma pueda detenerse sin agregar más números.",
        
        // Pregunta 4
        "La recursión utiliza una estructura que sigue el principio de 'último en entrar, primero en salir' (LIFO).",
        
        // Pregunta 5
        "El proceso de resolver las llamadas recursivas desde la última hasta la primera se llama desapilamiento.",
        
        // Pregunta 6
        "La condición que detiene las llamadas recursivas en una función se llama caso base.",
        
        // Pregunta 7
        "Si una función recursiva no tiene un caso base, las llamadas continuarán indefinidamente, causando un bucle infinito.",
        
        // Pregunta 8
        "Una ventaja de la recursión es que puede simplificar el código, haciéndolo más legible y fácil de mantener.",
        
        // Pregunta 9
        "Problemas como la secuencia de Fibonacci son adecuados para resolver con recursión debido a su naturaleza recurrente.",
        
        // Pregunta 10
        "Cuando una función se llama a sí misma varias veces en cada nivel, esto se denomina recursión múltiple.",
        
        // Pregunta 11
        "El riesgo principal de usar recursión con muchas llamadas es provocar un 'stack overflow' o desbordamiento de pila.",
        
        // Pregunta 12
        "En la función Fibonacci, un caso base inapropiado sería un valor de 'n' que no detiene la recursión adecuadamente.",
        
        // Pregunta 13
        "El caso base es fundamental para evitar recursiones infinitas en una función recursiva.",
        
        // Pregunta 14
        "Durante el desapilamiento, las llamadas recursivas comienzan a resolver sus operaciones pendientes en orden inverso.",
        
        // Pregunta 15
        "El caso base al invertir una cadena ocurre cuando la cadena está vacía; es entonces cuando la recursión se detiene.",
        
        // Pregunta 16
        "Cuando una función se llama a sí misma directamente, se produce una recursión directa.",
        
        // Pregunta 17
        "La estructura de control similar a la recursión es el bucle 'while', ya que repite acciones mientras una condición es verdadera."
    };

    private string[] tipsNivel2 = new string[]
    {
        // Pregunta 1
        "Al calcular el producto de una lista recursivamente, cada llamada procesa un elemento y avanza al siguiente.",
        
        // Pregunta 2
        "En sumaNaturales, cada llamada suma 'n' y llama recursivamente con 'n - 1' hasta llegar al caso base.",
        
        // Pregunta 3
        "En dobleSuma, se suma el doble de 'n' en cada llamada, disminuyendo 'n' hasta alcanzar el caso base.",
        
        // Pregunta 4
        "SumaCuadrado suma el cuadrado de 'n' en cada llamada recursiva hasta que 'n' es cero.",
        
        // Pregunta 5
        "Al calcular potencias recursivamente, el exponente disminuye en cada llamada hasta llegar a cero.",
        
        // Pregunta 6
        "SumMltplo suma 'm' repetidamente 'n' veces usando llamadas recursivas que disminuyen 'n'.",
        
        // Pregunta 7
        "Durante el desapilamiento de sumCubos, se suman los cubos de 'n' para obtener el resultado total.",
        
        // Pregunta 8
        "En sumMuDcr, las llamadas recursivas suman 'm' mientras 'n' decrece hasta el caso base.",
        
        // Pregunta 9
        "En potBaseFija, los resultados parciales se multiplican por la base durante el desapilamiento.",
        
        // Pregunta 10
        "ProdAlternante multiplica 'n' en cada llamada; presta atención si hay cambios de signo.",
        
        // Pregunta 11
        "En factorial, las llamadas recursivas se apilan disminuyendo 'n' hasta que 'n <= 1'.",
        
        // Pregunta 12
        "ContarDos recorre el arreglo recursivamente, incrementando el conteo cuando encuentra un '2'.",
        
        // Pregunta 13
        "BusMin compara el elemento actual con el mínimo encontrado en las llamadas recursivas anteriores.",
        
        // Pregunta 14
        "ContVocales analiza cada carácter de la cadena recursivamente para contar las vocales.",
        
        // Pregunta 15
        "Al invertir una cadena, cada llamada recursiva procesa el último carácter y continúa con el resto."
    };

    private string[] tipsNivel3 = new string[]
    {
        // Pregunta 1
        "En el desapilamiento de prodLis, los resultados parciales se multiplican para obtener el producto total.",
        
        // Pregunta 2
        "Durante el desapilamiento de sumNat, se suman los resultados parciales para obtener la suma total.",
        
        // Pregunta 3
        "En dobleSum, cada llamada devuelve '2 * n' sumado al resultado de las llamadas recursivas anteriores.",
        
        // Pregunta 4
        "En sumCuad, los cuadrados de 'n' se suman durante el desapilamiento para acumular el resultado final.",
        
        // Pregunta 5
        "Al desapilar en potencia, los resultados parciales se multiplican por la base para calcular la potencia total.",
        
        // Pregunta 6
        "En sumMltplo, se suman múltiplos de 'm' durante el desapilamiento para obtener el resultado final.",
        
        // Pregunta 7
        "En sumCubos, los cubos de 'n' se suman progresivamente durante el desapilamiento.",
        
        // Pregunta 8
        "Durante el desapilamiento de sumMuDcr, se acumulan las sumas de 'm' con 'n' decreciente.",
        
        // Pregunta 9
        "En potBaseFija, los resultados se multiplican por la base fija durante el desapilamiento para calcular la potencia.",
        
        // Pregunta 10
        "En prodAlternante, los productos y signos se combinan durante el desapilamiento para obtener el resultado final.",
        
        // Pregunta 11
        "En factorial, los resultados parciales se multiplican al desapilar para obtener el factorial completo.",
        
        // Pregunta 12
        "ContarDos suma las ocurrencias de '2' durante el desapilamiento en cada llamada recursiva.",
        
        // Pregunta 13
        "ContVocales acumula el conteo de vocales durante el desapilamiento para obtener el total.",
        
        // Pregunta 14
        "Al invertir una cadena recursivamente, los caracteres se concatenan en orden inverso durante el desapilamiento."
    };

    public TextMeshProUGUI tipTexto; 

    private string[] currentTips;
    private PlayerDataManager dataManager;

    void Start()
    {
        dataManager = PlayerDataManager.Instance;
        CargarTips();
        MostrarTipEspecifico();
    }

    private void CargarTips()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        if (sceneName == "EndLevel1")
        {
            currentTips = tipsNivel1;
        }
        else if (sceneName == "EndLevel2")
        {
            currentTips = tipsNivel2;
        }
        else if (sceneName == "EndLevel3")
        {
            currentTips = tipsNivel3;
        }
        else
        {
            Debug.LogWarning("Nombre de escena no reconocido para cargar tips.");
            currentTips = new string[] { "No hay tips disponibles para este nivel." };
            return;
        }
    }

    public void MostrarTipEspecifico()
    {
        if (dataManager == null || dataManager.playerData == null)
        {
            Debug.LogWarning("No hay datos del jugador disponibles.");
            tipTexto.text = "No hay tips disponibles.";
            return;
        }

        int indicePregunta = dataManager.playerData.lastIncorrectQuestionIndex;

        if (indicePregunta == -1)
        {
            tipTexto.text = "¡Felicidades! No has cometido errores.";
            return;
        }

        if (currentTips != null && currentTips.Length > 0)
        {
            if (indicePregunta >= 0 && indicePregunta < currentTips.Length)
            {
                tipTexto.text = currentTips[indicePregunta];
            }
            else
            {
                Debug.LogWarning("Índice de pregunta fuera de rango o no válido.");
                tipTexto.text = "No hay tips disponibles para esta pregunta.";
            }
        }
        else
        {
            tipTexto.text = "No hay tips disponibles.";
        }
    }
}