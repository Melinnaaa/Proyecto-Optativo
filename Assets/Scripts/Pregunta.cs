using System.Collections.Generic;
using UnityEngine;
using System.Linq;


public class Pregunta
{
    public string TextoPregunta { get; set; } // Texto de la pregunta
    public string CodigoTexto { get; set; } // Código relacionado con la pregunta (si aplica)
    public Dictionary<string, int> AlternativasConPosicion { get; set; } // Alternativas de la pregunta y sus posiciones
    public List<string> PilaAdicionalContenido { get; set; } // Contenido para la pila adicional de bricks, en un orden específico
}

