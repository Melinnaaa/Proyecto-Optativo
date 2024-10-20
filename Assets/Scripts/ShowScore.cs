using UnityEngine;
using UnityEngine.UI;

public class ShowScore : MonoBehaviour
{
    public TextMesh finalScoreText;

    void Start()
    {
        if (GameData.finalScore != null)
        {
            finalScoreText.text = GameData.finalScore.ToString();
        }
        else
        {
            finalScoreText.text = "Puntaje no disponible";
        }
    }
}
