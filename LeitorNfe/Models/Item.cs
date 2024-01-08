using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LeitorNfe.Models;

public class Item
{
    [Key]
    public int Id { get; set; }
    public int Numero { get; set; }
    public string CodigoProduto { get; set; }
    public string Nome { get; set; }
    public decimal QuantidadeComprada { get; set; }
    public decimal ValorUnitario { get; set; }
    public decimal ValorTotal { get; set; }
    public virtual ICollection<NotaItem> NotaItems { get; set; }

    public void loadXml(XElement item)
    {
        Console.WriteLine($"Item: {item}");
        XNamespace ns = "http://www.portalfiscal.inf.br/nfe";
        Numero = int.Parse(item.Attribute("nItem")?.Value ?? "0"); 
        CodigoProduto = item.Descendants(ns + "cProd").FirstOrDefault()!.Value;
        Nome = item.Descendants(ns + "xProd").FirstOrDefault()!.Value;
        QuantidadeComprada = decimal.Parse(item.Descendants(ns + "qCom").FirstOrDefault()!.Value);
        ValorUnitario = decimal.Parse(item.Descendants(ns + "vUnCom").FirstOrDefault()!.Value);
        ValorTotal = decimal.Parse(item.Descendants(ns + "vProd").FirstOrDefault()!.Value);
    }
}