using System.Collections.Generic;

[System.Serializable]
public class PlayerData
{
    public string playerName;
    public int lastIncorrectQuestionIndex;

    // Lista para almacenar todas las respuestas del jugador
    public List<RegistroPregunta> respuestas = new List<RegistroPregunta>();

    // Datos adicionales, como errores y niveles completados
    public int level1Errors;
    public int level2Errors;
    public int level3Errors;

    public bool level1Completed;
    public bool level2Completed;
    public bool level3Completed;
}
