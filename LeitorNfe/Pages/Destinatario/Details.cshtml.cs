using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LeitorNfe.Data;
using LeitorNfe.Models;

namespace LeitorNfe.Pages.Destinatario
{
    public class DetailsModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public DetailsModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public Models.Destinatario Destinatario { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var destinatario = await _context.Destinatarios.FirstOrDefaultAsync(m => m.Id == id);
            if (destinatario == null)
            {
                return NotFound();
            }
            else
            {
                Destinatario = destinatario;
            }
            return Page();
        }
    }
}
