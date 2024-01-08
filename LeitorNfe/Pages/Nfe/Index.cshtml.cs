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

        public async Task OnGetAsync()
        {
            NotaFiscal = await _context.NotaFiscals
                .Include(n => n.Destinatario)
                .Include(n => n.Emitente).ToListAsync();
        }
    }
}
