using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LeitorNfe.Data;
using LeitorNfe.Models;

namespace LeitorNfe.Pages.Pedido
{
    public class CreateModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public CreateModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public PedidoCompra PedidoCompra { get; set; } = default!;

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _context.PedidoCompras.Add(PedidoCompra);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
