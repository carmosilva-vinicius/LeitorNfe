
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace LeitorNfe.Models;

public class PedidoCompra
{
    [Key]
    public int Id { get; set; }
    [DisplayName("Número")]
    [Required(ErrorMessage = "O campo Numero é obrigatório.")] 
    [RegularExpression("^[0-9]*$", ErrorMessage = "O campo Numero deve conter apenas dígitos numéricos.")]
    public string Numero { get; set; }
    [DisplayName("Descrição")]
    [MaxLength(255, ErrorMessage = "O campo Descricao deve conter no máximo 255 caracteres.")]
    public string? Descricao { get; set; }
    public virtual ICollection<NotaFiscal> NotasFiscais { get; set; }
}