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
    public class DetailsModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public DetailsModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

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
    }
}
