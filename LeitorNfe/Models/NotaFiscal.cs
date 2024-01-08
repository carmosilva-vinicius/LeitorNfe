using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LeitorNfe.Models;

public class NotaFiscal
{
    public void loadXml(XDocument xml)
    {
        XNamespace ns = "http://www.portalfiscal.inf.br/nfe";
        Numero = int.Parse(xml.Descendants(ns + "nNF").FirstOrDefault()!.Value);
        ChaveAcesso = xml.Descendants(ns + "chNFe").FirstOrDefault()!.Value;
        DataEmissao = DateTime.Parse(xml.Descendants(ns + "dhEmi").FirstOrDefault()!.Value);
        Total = decimal.Parse(xml.Descendants(ns + "vNF").FirstOrDefault()!.Value);
    }
    
    [Key]
    public int Id { get; set; }
    [DisplayName("Número")]
    public int Numero { get; set; }
    [DisplayName("Chave de Acesso")]
    public string ChaveAcesso { get; set; }
    [DisplayName("Data de Emissão")]
    public DateTime DataEmissao { get; set; }
    public List<Item> Itens { get; set; }
    public decimal Total { get; set; }
    public int EmitenteId { get; set; }
    public int DestinatarioId { get; set; }
    public int? PedidoCompraId { get; set; }
    [DisplayName("Pedido de Compra")]
    public virtual PedidoCompra? PedidoCompra { get; set; }
    public virtual Emitente? Emitente { get; set; }
    [DisplayName("Destinatário")]
    public virtual Destinatario? Destinatario { get; set; }
    public virtual ICollection<NotaItem> NotaItems { get; set; }
}