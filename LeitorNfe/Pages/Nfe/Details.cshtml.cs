using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LeitorNfe.Data;
using LeitorNfe.Models;

namespace LeitorNfe.Pages.Nfe
{
    public class DetailsModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public DetailsModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public NotaFiscal NotaFiscal { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notafiscal = await _context.NotaFiscals.FirstOrDefaultAsync(m => m.Id == id);
            if (notafiscal == null)
            {
                return NotFound();
            }
            else
            {
                notafiscal.Emitente = await _context
                    .Emitentes.FirstOrDefaultAsync(m => m.Id == notafiscal.EmitenteId);
                notafiscal.Destinatario = await _context
                    .Destinatarios.FirstOrDefaultAsync(m => m.Id == notafiscal.DestinatarioId);
                notafiscal.Destinatario!.Endereco = await _context
                    .Enderecos.FirstOrDefaultAsync(m => m.Id == notafiscal.Destinatario.EnderecoId);
                notafiscal.Emitente!.Endereco = await _context
                    .Enderecos.FirstOrDefaultAsync(m => m.Id == notafiscal.Emitente.EnderecoId);
                
                //load itens
                notafiscal.Itens = await _context.NotaItems
                    .Where(ni => ni.NotaFiscalId == notafiscal.Id)
                    .Select(ni => ni.Item)
                    .ToListAsync();
                NotaFiscal = notafiscal;
            }
            return Page();
        }
    }
}
