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

namespace LeitorNfe.Pages.Pedido
{
    public class EditModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public EditModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public PedidoCompra PedidoCompra { get; set; } = default!;

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidocompra =  await _context.PedidoCompras.FirstOrDefaultAsync(m => m.Id == id);
            if (pedidocompra == null)
            {
                return NotFound();
            }
            PedidoCompra = pedidocompra;
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

            _context.Attach(PedidoCompra).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PedidoCompraExists(PedidoCompra.Id))
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

        private bool PedidoCompraExists(int id)
        {
            return _context.PedidoCompras.Any(e => e.Id == id);
        }
    }
}
