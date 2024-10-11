using UnityEngine;
using TMPro; // Para usar TextMeshPro si es necesario
using System.Collections.Generic;

public class BlockManager : MonoBehaviour
{
    public QuestionBank questionBank; // Referencia al banco de preguntas
    public GameObject[] blocks; // Arreglo de bloques en la escena

    private Question currentQuestion;

    private void Start()
    {
        AssignQuestionToBlocks();
    }

    private void AssignQuestionToBlocks()
    {
        // Obtener una pregunta aleatoria del banco de preguntas
        currentQuestion = questionBank.GetRandomQuestion();

        // Mezclar las respuestas para distribuirlas de manera aleatoria en los bloques
        List<string> shuffledAnswers = new List<string>(currentQuestion.answers);
        shuffledAnswers = ShuffleList(shuffledAnswers);

        // Asignar las respuestas a los bloques
        for (int i = 0; i < blocks.Length; i++)
        {
            if (i < shuffledAnswers.Count)
            {
                // Obtener el componente TextMesh del bloque
                TextMesh textMesh = blocks[i].GetComponentInChildren<TextMesh>();
                if (textMesh != null)
                {
                    textMesh.text = shuffledAnswers[i]; // Asignar la respuesta al texto del bloque
                }
                else
                {
                    Debug.LogError("No se encontró TextMesh en el bloque " + blocks[i].name);
                }

                // Asignar la respuesta al script del bloque
                Brick brickScript = blocks[i].GetComponent<Brick>();
                if (brickScript != null)
                {
                    brickScript.SetAnswer(shuffledAnswers[i]);
                }
                else
                {
                    Debug.LogError("No se encontró el script Brick en el bloque " + blocks[i].name);
                }
            }
        }
    }

    // Método para mezclar la lista de respuestas aleatoriamente
    private List<string> ShuffleList(List<string> list)
    {
        for (int i = 0; i < list.Count; i++)
        {
            string temp = list[i];
            int randomIndex = Random.Range(i, list.Count);
            list[i] = list[randomIndex];
            list[randomIndex] = temp;
        }
        return list;
    }
}
