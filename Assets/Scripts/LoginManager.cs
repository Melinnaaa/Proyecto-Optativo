using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro; 
using System.IO;
using UnityEngine.UI;

public class LoginManager : MonoBehaviour
{
    public TMP_InputField playerNameInput;
    public Button newGameButton;
    public Button loadGameButton;
    public TextMeshProUGUI errorMessage;

    private string filePath;

    private void Start()
    {
        filePath = Path.Combine(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments), "FractalFrenzy_PlayerData.json");
        loadGameButton.interactable = File.Exists(filePath);
        errorMessage.gameObject.SetActive(false);
    }

    public void StartNewGame()
    {
        string playerName = playerNameInput.text;

        if (string.IsNullOrEmpty(playerName))
        {
            ShowErrorMessage("Por favor, ingresa un nombre.");
            return;
        }

        // Inicializa los datos del jugador con los campos actualizados
        PlayerDataManager.Instance.playerData = new PlayerData 
        { 
            playerName = playerName, 
            level1Errors = 0, 
            level2Errors = 0, 
            level3Errors = 0,
            level1Completed = false,
            level2Completed = false,
            level3Completed = false
        };

        PlayerDataManager.Instance.SaveData();
        SceneManager.LoadScene("LevelsMenu");
    }

    public void LoadGame()
    {
        if (File.Exists(filePath))
        {
            PlayerDataManager.Instance.LoadData();
            SceneManager.LoadScene("LevelsMenu");
        }
        else
        {
            ShowErrorMessage("No se encontraron datos guardados.");
        }
    }

    private void ShowErrorMessage(string message)
    {
        errorMessage.text = message;
        errorMessage.gameObject.SetActive(true);
    }
}
