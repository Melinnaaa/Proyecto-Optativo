using UnityEngine;
using System.IO;
using System;

public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }
    public PlayerData playerData;

    private string filePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Evita instancias duplicadas
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Hace que persista entre escenas

            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FractalFrenzy_PlayerData.json");
            LoadData();
        }
    }

    public void SaveData()
    {
        if (playerData == null) playerData = new PlayerData();
        string json = JsonUtility.ToJson(playerData);
        File.WriteAllText(filePath, json);
        Debug.Log("Datos guardados: " + json);
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            string json = File.ReadAllText(filePath);
            playerData = JsonUtility.FromJson<PlayerData>(json);
            Debug.Log("Datos cargados: " + json);
        }
        else
        {
            Debug.Log("No se encontraron datos previos. Creando datos predeterminados.");
            playerData = new PlayerData { playerName = "Jugador" };
            SaveData();
        }
    }
}
