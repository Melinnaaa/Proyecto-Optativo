﻿using UnityEngine;
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

        // Guardar los datos en formato JSON sin formateo para evitar problemas de caracteres
        string json = JsonUtility.ToJson(playerData);

        try
        {
            File.WriteAllText(filePath, json);
            Debug.Log("Datos guardados correctamente.");
        }
        catch (Exception e)
        {
            Debug.LogError("Error al guardar los datos: " + e.Message);
        }
    }

    public void LoadData()
    {
        if (File.Exists(filePath))
        {
            try
            {
                string json = File.ReadAllText(filePath);
                playerData = JsonUtility.FromJson<PlayerData>(json);
                Debug.Log("Datos cargados correctamente.");
            }
            catch (Exception e)
            {
                Debug.LogError("Error al cargar los datos: " + e.Message);
                playerData = new PlayerData(); // Crear datos nuevos en caso de error
            }
        }
        else
        {
            playerData = new PlayerData(); // Crear datos nuevos si no hay archivo
            Debug.Log("No se encontraron datos previos. Se inicializan datos nuevos.");
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
}
