using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Xml.Linq;
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
        private NotaFiscal _notaAtual;

        public EditModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        [BindProperty]
        public IFormFile FileUpload { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            _notaAtual = _context.NotaFiscals.Find(id);
            TempData["_notaAtual"] = _notaAtual;
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            _notaAtual = (NotaFiscal)TempData["_notaAtual"];
            var destinatarioAtual = _notaAtual.Destinatario;
            var emitenteAtual = _notaAtual.Emitente;
            var pedidoAtual = _notaAtual.PedidoCompra;
            var itemsAtual = _notaAtual.Itens;
            var endereceDestinatarioAtual = destinatarioAtual.Endereco;
            var endereceEmitenteAtual = emitenteAtual.Endereco;


            if (FileUpload != null && FileUpload.Length > 0)
            {
                var reader = new StreamReader(FileUpload.OpenReadStream());
                var content = await reader.ReadToEndAsync();

                XNamespace ns = "http://www.portalfiscal.inf.br/nfe";
                XDocument xml = XDocument.Parse(content);

                var emit = xml.Descendants(ns + "emit").FirstOrDefault();
                var emitEnd = emit?.Descendants(ns + "enderEmit").FirstOrDefault();
                var dest = xml.Descendants(ns + "dest").FirstOrDefault();
                var destEnd = dest?.Descendants(ns + "enderDest").FirstOrDefault();

                
                
            }
            return RedirectToPage("./Index");
        }

    }
}
