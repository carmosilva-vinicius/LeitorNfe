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
    public class IndexModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public IndexModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<PedidoCompra> PedidoCompra { get;set; } = default!;

        public async Task OnGetAsync()
        {
            PedidoCompra = await _context.PedidoCompras.ToListAsync();
        }
    }
}
