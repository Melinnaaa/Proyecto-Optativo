[System.Serializable]
public class PlayerData
{
    public string playerName;
    
    // Errores en cada nivel
    public int level1Errors;
    public int level2Errors;
    public int level3Errors;
    
    // Indicadores de niveles completados
    public bool level1Completed;
    public bool level2Completed;
    public bool level3Completed;
}
