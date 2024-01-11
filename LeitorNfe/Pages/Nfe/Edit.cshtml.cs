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

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
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

                Models.Endereco emitenteEndereco = new Models.Endereco();
                emitenteEndereco.loadXml(emitEnd);
                emitenteEndereco.Id = endereceEmitenteAtual.Id;
                _context.Enderecos.Update(emitenteEndereco);
                await _context.SaveChangesAsync();

                Models.Emitente emitente = new Models.Emitente();
                emitente.loadXml(emit);
                emitente.Id = emitenteAtual.Id;
                emitente.EnderecoId = emitenteEndereco.Id;
                _context.Emitentes.Update(emitente);

                Models.Endereco destinatarioEndereco = new Models.Endereco();
                destinatarioEndereco.loadXml(destEnd);
                destinatarioEndereco.Id = endereceDestinatarioAtual.Id;
                _context.Enderecos.Update(destinatarioEndereco);
                await _context.SaveChangesAsync();

                Models.Destinatario destinatario = new Models.Destinatario();
                destinatario.loadXml(dest);
                destinatario.Id = destinatarioAtual.Id;
                destinatario.EnderecoId = destinatarioEndereco.Id;
                _context.Destinatarios.Update(destinatario);
                await _context.SaveChangesAsync();

                NotaFiscal notaFiscal = new NotaFiscal();
                notaFiscal.loadXml(xml);
                notaFiscal.Id = _notaAtual.Id;
                notaFiscal.EmitenteId = emitente.Id;
                notaFiscal.DestinatarioId = destinatario.Id;
                _context.NotaFiscals.Update(notaFiscal);
                await _context.SaveChangesAsync();

                var itens = xml.Descendants(ns + "det");
                foreach (var item in itens)
                {
                    Models.Item i = new Models.Item();
                    i.loadXml(item);
                    _context.Items.Add(i);
                    await _context.SaveChangesAsync();

                    NotaItem ni = new NotaItem();
                    ni.ItemId = i.Id;
                    ni.NotaFiscalId = notaFiscal.Id;
                    _context.NotaItems.Add(ni);
                    await _context.SaveChangesAsync();
                }

                _context.Attach(notaFiscal).State = EntityState.Modified;
                await _context.SaveChangesAsync();
                
            }
            return RedirectToPage("./Index");
        }

    }
}
