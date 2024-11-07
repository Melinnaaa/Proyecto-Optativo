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
            LoadData(); // Cargar datos solo si existe el archivo
        }
    }

    public void SaveData()
    {
        if (playerData == null) playerData = new PlayerData();

        // Guardar los datos en formato JSON con sangría
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
            // No se crea un archivo predeterminado aquí
            playerData = null;
            Debug.Log("No se encontraron datos previos.");
        }
    }

    public void RegistrarDatosJugador(string pregunta, List<string> alternativas, string respuestaJugador, bool siFueCorrectaONo, float tiempoDeRespuesta)
    {
        if (playerData == null) 
        {
            playerData = new PlayerData { playerName = "Jugador" }; // Crear datos solo si es necesario
        }

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
        // Formateo básico de JSON para una mejor legibilidad
        json = json.Replace("{", "{\n\t");
        json = json.Replace("}", "\n}");
        json = json.Replace("[", "[\n\t");
        json = json.Replace("]", "\n]");
        json = json.Replace(",", ",\n\t");
        return json;
    }
}
