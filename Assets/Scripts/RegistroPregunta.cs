using System.Collections.Generic;

[System.Serializable]
public class RegistroPregunta
{
    public string timestamp;            // Fecha y hora en que se respondió la pregunta
    public string pregunta;             // Texto de la pregunta
    public List<string> alternativas;   // Lista de alternativas de respuesta
    public string respuestaJugador;     // Respuesta dada por el jugador
    public bool siFueCorrectaONo;       // Indica si la respuesta fue correcta o no
    public float tiempoDeRespuesta;     // Tiempo que tomó responder la pregunta
}