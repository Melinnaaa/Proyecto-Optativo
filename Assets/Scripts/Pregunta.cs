using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class Pregunta
{
    public string TextoPregunta { get; set; }
    public string CodigoTexto { get; set; }
    public Dictionary<string, int> AlternativasConPosicion { get; set; }

        
}
