using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using LeitorNfe.Data;
using LeitorNfe.Models;

namespace LeitorNfe.Pages.Nfe
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
        ViewData["DestinatarioId"] = new SelectList(_context.Destinatarios, "Id", "Id");
        ViewData["EmitenteId"] = new SelectList(_context.Emitentes, "Id", "Id");
            return Page();
        }

        // [BindProperty]
        // public NotaFiscal NotaFiscal { get; set; } = default!;
        [BindProperty]
        public IFormFile FileUpload { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            
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
                
                Console.WriteLine($"emit: {emit}\nemitEnd: {emitEnd}\n\ndest: {dest}\ndestEnd: {destEnd}");
                
                Models.Endereco emitenteEndereco = new Models.Endereco();
                emitenteEndereco.loadXml(emitEnd);
                _context.Enderecos.Add(emitenteEndereco);
                await _context.SaveChangesAsync();
                
                Models.Emitente emitente = new Models.Emitente();
                emitente.loadXml(xml);
                emitente.EnderecoId = emitenteEndereco.Id;
                _context.Emitentes.Add(emitente);
                await _context.SaveChangesAsync();
                
                Models.Endereco destinatarioEndereco = new Models.Endereco();
                destinatarioEndereco.loadXml(destEnd);
                _context.Enderecos.Add(destinatarioEndereco);
                await _context.SaveChangesAsync();
                
                Models.Destinatario destinatario = new Models.Destinatario();
                destinatario.loadXml(xml);
                destinatario.EnderecoId = destinatarioEndereco.Id;
                _context.Destinatarios.Add(destinatario);
                await _context.SaveChangesAsync();
                
                NotaFiscal notaFiscal = new NotaFiscal();
                notaFiscal.loadXml(xml);
                notaFiscal.EmitenteId = emitente.Id;
                notaFiscal.DestinatarioId = destinatario.Id;
                _context.NotaFiscals.Add(notaFiscal);
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
            }

            
            return RedirectToPage("./Index");
            
        }
    }
}
