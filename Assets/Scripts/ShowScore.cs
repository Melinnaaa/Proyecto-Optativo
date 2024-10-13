using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public Text scoreText;

    void Start()
    {
        int finalScore = ScoreManager.Instance.GetFinalScore(); // Recuperar el puntaje
        scoreText.text = "Puntaje Final: " + finalScore.ToString();
    }
}
