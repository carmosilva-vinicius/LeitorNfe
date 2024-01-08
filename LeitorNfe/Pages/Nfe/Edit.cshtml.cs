using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using LeitorNfe.Data;
using LeitorNfe.Models;

namespace LeitorNfe.Pages.Nfe
{
    public class EditModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public EditModel(LeitorNfe.Data.ApplicationDbContext context)
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

            var notafiscal =  await _context.NotaFiscals.FirstOrDefaultAsync(m => m.Id == id);
            if (notafiscal == null)
            {
                return NotFound();
            }
            NotaFiscal = notafiscal;
           ViewData["DestinatarioId"] = new SelectList(_context.Destinatarios, "Id", "Id");
           ViewData["EmitenteId"] = new SelectList(_context.Emitentes, "Id", "Id");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.Attach(NotaFiscal).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotaFiscalExists(NotaFiscal.Id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return RedirectToPage("./Index");
        }

        private bool NotaFiscalExists(int id)
        {
            return _context.NotaFiscals.Any(e => e.Id == id);
        }
    }
}
