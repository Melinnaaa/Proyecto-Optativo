using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Question
{
    public string questionText; // Texto de la pregunta
    public List<string> answers; // Lista de respuestas posibles
    public int correctAnswerIndex; // Índice de la respuesta correcta
}

public class QuestionBank : MonoBehaviour
{
    public List<Question> questions; // Lista de todas las preguntas

    private void Start()
    {
        // Ejemplo de preguntas agregadas manualmente
        questions = new List<Question>
        {
            new Question
            {
                questionText = "¿Cuál es la capital de Francia?",
                answers = new List<string> { "París", "Londres", "Madrid", "Berlín" },
                correctAnswerIndex = 0
            },
            new Question
            {
                questionText = "¿Cuál es el resultado de 5 + 3?",
                answers = new List<string> { "5", "8", "7", "9" },
                correctAnswerIndex = 1
            }
        };
    }

    public Question GetRandomQuestion()
    {
        int randomIndex = Random.Range(0, questions.Count);
        return questions[randomIndex];
    }
}
