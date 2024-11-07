using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public TextMesh finalScoreText;

    void Start()
    {
        if (ScoreManager.Instance != null)
        {
            finalScoreText.text = ScoreManager.Instance.GetFinalScore().ToString();
        }
        else
        {
            finalScoreText.text = "Puntaje no disponible";
            Debug.LogError("ScoreManager.Instance es null. Asegúrate de que ScoreManager está en la escena.");
        }
    }
}
