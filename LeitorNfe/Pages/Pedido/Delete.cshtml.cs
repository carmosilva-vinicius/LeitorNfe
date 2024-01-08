using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LeitorNfe.Data;
using LeitorNfe.Models;

namespace LeitorNfe.Pages.Pedido
{
    public class DeleteModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public DeleteModel(LeitorNfe.Data.ApplicationDbContext context)
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

            var pedidocompra = await _context.PedidoCompras.FirstOrDefaultAsync(m => m.Id == id);

            if (pedidocompra == null)
            {
                return NotFound();
            }
            else
            {
                PedidoCompra = pedidocompra;
            }
            return Page();
        }

        public async Task<IActionResult> OnPostAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var pedidocompra = await _context.PedidoCompras.FindAsync(id);
            if (pedidocompra != null)
            {
                PedidoCompra = pedidocompra;
                _context.PedidoCompras.Remove(PedidoCompra);
                await _context.SaveChangesAsync();
            }

            return RedirectToPage("./Index");
        }
    }
}
