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
        private int _notaFiscalId;

        public CreateModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult OnGet(int? id)
        {
            if(id is null or <= 0)
            {
                return RedirectToPage("../Nfe/Index");
            }
            _notaFiscalId = (int)id;
            TempData["_notaFiscalId"] = _notaFiscalId;
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
            
            _notaFiscalId = (int)TempData["_notaFiscalId"];
            var notaFiscal = _context.NotaFiscals.Find(_notaFiscalId);
            notaFiscal.PedidoCompraId = PedidoCompra.Id;
            _context.NotaFiscals.Update(notaFiscal);
            await _context.SaveChangesAsync();

            return RedirectToPage("../Nfe/Index");
        }
    }
}
