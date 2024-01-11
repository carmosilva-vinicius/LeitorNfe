using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LeitorNfe.Models;

public class Emitente
{
    [Key]
    public int Id { get; set; }
    public string Nome { get; set; }
    public string CNPJ { get; set; }
    public int EnderecoId { get; set; }
    [DisplayName("Endereço")]
    public virtual Endereco? Endereco { get; set; }
    public virtual ICollection<NotaFiscal> NotasFiscais { get; set; }

    public void loadXml(XElement? xml)
    {
        XNamespace ns = "http://www.portalfiscal.inf.br/nfe";
        Nome = xml.Descendants(ns + "xNome").FirstOrDefault()?.Value;
        CNPJ = xml.Descendants(ns + "CNPJ").FirstOrDefault()?.Value;
    }
}