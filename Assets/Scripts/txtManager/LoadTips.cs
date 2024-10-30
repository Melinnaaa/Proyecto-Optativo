using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class LoadTips : MonoBehaviour
{
    private string[] tipsNivel1 = new string[]
    {
        "En el factorial, el caso base es un valor que, al multiplicarse, no cambia el resultado.",
        "Piensa en el valor que define el inicio de la función factorial. Es el número que detiene el cálculo de productos.",
        "El factorial se detiene en un valor específico para evitar un ciclo infinito de multiplicaciones.",
        "¿Qué valor del exponente hace que cualquier número elevado a él no requiera multiplicaciones adicionales?",
        "Reflexiona sobre qué valor en \"exp\" permite que la función potencia se detenga sin más cálculos.",
        "Para la potencia, el caso base define el exponente mínimo que no requiere más operación.",
        "Imagina el valor de \"n\" que permitiría que la suma de los números naturales se detenga inmediatamente.",
        "Piensa en el número más pequeño que hace que la suma no necesite sumar más valores.",
        "La función de sumaNaturales tiene un caso base que marca el fin de la suma; ¿cuál podría ser este valor?",
        "La recursión se basa en una estructura que guarda cada llamada hasta que todas se completan.",
        "Recuerda que en recursión, las llamadas se resuelven en orden inverso. ¿Qué estructura permite este orden?",
        "Piensa en una estructura que sigue el principio de \"último en entrar, primero en salir\" (LIFO).",
        "Este proceso implica \"sacar\" los elementos en el orden inverso al que entraron.",
        "La recursión resuelve sus llamadas \"sacando\" cada paso en el orden opuesto al de la entrada.",
        "En recursión, hay un proceso que libera las llamadas desde la última realizada hasta la primera. ¿Cómo se llama?",
        "Cada función recursiva necesita una condición para no seguir llamándose infinitamente.",
        "Este punto es esencial para que la función recursiva se detenga de forma controlada. ¿Cómo se llama?",
        "Sin esta parte, una función recursiva caería en un bucle infinito. ¿Cuál es su nombre?"
    };

    private string[] tipsNivel2 = new string[]
    {
        "Recuerda que en la recursión, cada llamada se apila en el stack hasta que se llega a un caso base.",
        "Piensa en el orden en el que las llamadas se apilan en el stack: es como resolver el problema paso a paso.",
        "Para el cálculo de fibonacci(4), cada llamada a fibonacci(n) se descompone en llamadas a fibonacci(n-1) y fibonacci(n-2).",
        "En el cálculo de productoLista({2, 3, 4, 5}), cada llamada a productoLista(n) se apila hasta que se llega a productoLista(0).",
        "Recuerda que el máximo común divisor (mcd) se calcula usando recursión y que cada llamada reduce el problema.",
        "Cuando calculas potencia(3, 4), cada llamada reduce el exponente hasta llegar a potencia(3, 0), el caso base.",
        "En la función buscar, cada llamada recursiva reduce el tamaño del arreglo hasta encontrar el elemento o llegar al caso base.",
        "Para sumaDigitos(4321), cada llamada quita un dígito hasta que solo queda un dígito o se llega a cero.",
        "Piensa en cada llamada recursiva como una nueva capa en el stack que debe completarse antes de regresar a la anterior.",
        "El stack de llamadas permite recordar el estado de cada cálculo parcial, volviendo paso a paso cuando llega al caso base."
    };


    private string[] tipsNivel3 = new string[]
    {
        "Recuerda que el desapilamiento en la recursión ocurre cuando se alcanzan los valores base y se resuelven las llamadas pendientes.",
        "Para fibonacci(4), el desapilamiento sigue un orden específico al devolver los resultados acumulados.",
        "En productoLista, cada llamada a productoLista(n) devuelve el valor del producto parcial hasta el índice 0.",
        "El cálculo del máximo común divisor (mcd) también se resuelve durante el desapilamiento, devolviendo el valor final.",
        "En potencia(3, 4), cada resultado parcial se multiplica con la base durante el desapilamiento hasta obtener el exponente deseado.",
        "Para buscar un número en un arreglo, si se encuentra el elemento, las llamadas posteriores no se ejecutan.",
        "En sumaDigitos(4321), cada llamada suma un dígito parcial hasta que el número se reduce al valor total.",
        "Durante el desapilamiento, los valores se 'devuelven' a medida que se completan las llamadas recursivas de abajo hacia arriba.",
        "Cada paso del desapilamiento utiliza el resultado de la llamada recursiva más profunda para resolver la anterior.",
        "Recuerda que el desapilamiento permite resolver el problema en sentido inverso, acumulando el resultado de cada llamada."
    };

    public TextMeshProUGUI tipTexto; 

    private string[] currentTips;

    void Start()
    {
        CargarTips();
        MostrarTipAleatorio();
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
        }
    }

    public void MostrarTipAleatorio()
    {
        if (currentTips != null && currentTips.Length > 0)
        {
            int randomIndex = Random.Range(0, currentTips.Length);
            tipTexto.text = currentTips[randomIndex];
        }
    }
}
