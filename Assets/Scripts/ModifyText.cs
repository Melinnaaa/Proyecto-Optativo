using UnityEngine;

public class ModifyText : MonoBehaviour
{
    public TextMesh preguntaTexto; // TextMesh para mostrar la pregunta
    public TextMesh[] alternativasTextos; // Array de TextMesh para mostrar las alternativas

    private string[] preguntas = new string[]
    {
        "1. ¿Cuál es el caso base de la función factorial?",
        "2. ¿Cuál es el caso base de la función potencia?",
        "3. ¿Cuál es el caso base de la función sumaNaturales?",
        "4. ¿Qué estructura de datos se utiliza en la recursión para almacenar el estado de cada llamada?",
        "5. ¿Cómo se llama el proceso de resolver las llamadas recursivas desde la última a la primera?",
        "6. ¿Cómo se llama la parte de una función recursiva que detiene la recursión?"
    };

    private string[][] alternativas = new string[][]
    {
        new string[] { "n = 0", "n = 1", "n<=1", "n=2", "n<0", "no tiene caso base" },
        new string[] { "exp = 1", "exp = 2", "exp<=0", "exp = 0", "base = 0", "base = 1" },
        new string[] { "n = 1", "n = 0", "n<0", "n>1", "n = 2", "no tiene caso base" },
        new string[] { "Lista", "Pila (Stack)", "Cola", "Árbol", "Conjunto", "Grafo" },
        new string[] { "Desapilamiento", "Apilamiento", "Búsqueda", "Inserción", "Ordenamiento", "Recorrido" },
        new string[] { "Llamada recursiva", "Caso base", "Bucle infinito", "Subrutina", "Punto de control", "Retorno" }
    };

    // Respuestas correctas
    private char[] respuestasCorrectas = new char[] { 'b', 'd', 'b', 'b', 'b', 'b' };

    private int preguntaIndex;

    void Start()
    {
        CargarPreguntaAleatoria();
    }

    void CargarPreguntaAleatoria()
    {
        // Seleccionar una pregunta aleatoria
        preguntaIndex = Random.Range(0, preguntas.Length);

        // Asignar la pregunta
        preguntaTexto.text = preguntas[preguntaIndex];

        // Asignar las alternativas y respuestas a los bloques
        for (int i = 0; i < alternativasTextos.Length && i < alternativas[preguntaIndex].Length; i++)
        {
            alternativasTextos[i].text = alternativas[preguntaIndex][i];

            // Asignar respuesta correcta o incorrecta al bloque
            Brick brick = alternativasTextos[i].GetComponentInParent<Brick>(); // Asegúrate de que los bloques tengan este script
            if (brick != null)
            {
                // Comparar el índice de la alternativa con la respuesta correcta (restar 'a' para convertir de letra a índice)
                bool esCorrecto = (i == (respuestasCorrectas[preguntaIndex] - 'a'));
                brick.SetAnswer(alternativas[preguntaIndex][i], esCorrecto);
            }
        }
    }


    // Método para verificar si la respuesta seleccionada es correcta
    public void VerificarRespuesta(int indiceSeleccionado)
    {
        // Convertir el índice a letra para compararlo con la respuesta correcta
        char seleccion = (char)('a' + indiceSeleccionado);

        if (seleccion == respuestasCorrectas[preguntaIndex])
        {
            Debug.Log("Respuesta Correcta!");
            // Aquí puedes llamar a un método para finalizar el nivel o dar feedback de victoria
        }
        else
        {
            Debug.Log("Respuesta Incorrecta!");
            // Aquí puedes hacer que el bloque cambie a rojo o dar feedback de error
        }
    }
}
