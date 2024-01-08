using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LeitorNfe.Models;

public class Endereco
{
    [Key]
    public int Id { get; set; }
    public string Logradouro { get; set; }
    [DisplayName("Número")]
    public string Numero { get; set; }
    public string Bairro { get; set; }
    [DisplayName("Município")]
    public string Municipio { get; set; }
    public string UF { get; set; }
    public string CEP { get; set; }
    
    public void loadXml(XElement? endereco)
    {
        XNamespace ns = "http://www.portalfiscal.inf.br/nfe";
        
        Logradouro = endereco?.Descendants(ns + "xLgr").FirstOrDefault()?.Value;
        Numero = endereco?.Descendants(ns + "nro").FirstOrDefault()?.Value;
        Bairro = endereco?.Descendants(ns + "xBairro").FirstOrDefault()?.Value;
        Municipio = endereco?.Descendants(ns + "xMun").FirstOrDefault()?.Value;
        UF = endereco?.Descendants(ns + "UF").FirstOrDefault()?.Value;
        CEP = endereco?.Descendants(ns + "CEP").FirstOrDefault()?.Value;
    }
}