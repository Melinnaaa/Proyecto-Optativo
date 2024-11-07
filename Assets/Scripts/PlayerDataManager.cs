using UnityEngine;
using System.IO;
using System;
using System.Collections.Generic;
public class PlayerDataManager : MonoBehaviour
{
    public static PlayerDataManager Instance { get; private set; }
    public PlayerData playerData;

    private string filePath;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            filePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "FractalFrenzy_PlayerData.json");
            LoadData();
        }
    }

    public void SaveData()
    {
        if (playerData == null) playerData = new PlayerData();
        
        // Save the JSON data with custom formatting
        string json = JsonUtility.ToJson(playerData);
        string formattedJson = FormatJsonPretty(json);

        File.WriteAllText(filePath, formattedJson);
        Debug.Log("Datos guardados: " + formattedJson);
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

    public void RegistrarDatosJugador(string pregunta, List<string> alternativas, string respuestaJugador, bool siFueCorrectaONo, float tiempoDeRespuesta)
    {
        RegistroPregunta registro = new RegistroPregunta
        {
            timestamp = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"),
            pregunta = pregunta,
            alternativas = alternativas,
            respuestaJugador = respuestaJugador,
            siFueCorrectaONo = siFueCorrectaONo,
            tiempoDeRespuesta = tiempoDeRespuesta
        };

        playerData.respuestas.Add(registro);
        SaveData();
    }

    private string FormatJsonPretty(string json)
    {
        // A basic way to pretty-print JSON in Unity, though not as flexible as Newtonsoft
        json = json.Replace("{", "{\n\t");
        json = json.Replace("}", "\n}");
        json = json.Replace("[", "[\n\t");
        json = json.Replace("]", "\n]");
        json = json.Replace(",", ",\n\t");
        return json;
    }
}
