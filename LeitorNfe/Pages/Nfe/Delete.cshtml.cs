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
    public class DeleteModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public DeleteModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
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
                NotaFiscal = notafiscal;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var notafiscal = await _context.NotaFiscals.FindAsync(id);
            if (notafiscal != null)
            {
                NotaFiscal = notafiscal;
                _context.NotaFiscals.Remove(NotaFiscal);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
