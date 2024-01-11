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
    public class IndexModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public IndexModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<NotaFiscal> NotaFiscal { get;set; } = default!;

        public async Task OnGetAsync(string SearchNumero, DateTime? SearchData, string SearchEmitente, string SearchDestinatario)
        {
            NotaFiscal = await _context.NotaFiscals
                .Include(n => n.Destinatario)
                .Include(n => n.Emitente).ToListAsync();

            if (!String.IsNullOrEmpty(SearchNumero))
            {
                NotaFiscal = NotaFiscal.Where(s => s.Numero == Int32.Parse(SearchNumero)).ToList();
            }

            if (SearchData.HasValue)
            {
                NotaFiscal = NotaFiscal.Where(s => s.DataEmissao == SearchData).ToList();
            }

            if (!String.IsNullOrEmpty(SearchEmitente))
            {
                NotaFiscal = NotaFiscal.Where(s => s.Emitente!.Nome.Contains(SearchEmitente)).ToList();
            }

            if (!String.IsNullOrEmpty(SearchDestinatario))
            {
                NotaFiscal = NotaFiscal.Where(s => s.Destinatario!.Nome.Contains(SearchDestinatario)).ToList();
            }

            
        }

    }
}
