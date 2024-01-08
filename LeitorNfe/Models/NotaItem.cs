namespace LeitorNfe.Models;

public class NotaItem
{
    public int NotaFiscalId { get; set; }
    public virtual NotaFiscal NotaFiscal { get; set; }

    public int ItemId { get; set; }
    public virtual Item Item { get; set; }
}