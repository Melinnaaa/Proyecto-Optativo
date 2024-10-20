using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModifyTextLvl3 : MonoBehaviour, IModifyText
{
    public static ModifyTextLvl3 Instance { get; private set; }
    
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject); // Evitar duplicados
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Hacer que persista entre escenas
        }
    }

    public void CargarPreguntaAleatoria()
    {

    }

    public void ReiniciarPreguntas()
    {
    }
}

