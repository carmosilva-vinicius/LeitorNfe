using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using LeitorNfe.Data;
using LeitorNfe.Models;

namespace LeitorNfe.Pages.Item
{
    public class IndexModel : PageModel
    {
        private readonly LeitorNfe.Data.ApplicationDbContext _context;

        public IndexModel(LeitorNfe.Data.ApplicationDbContext context)
        {
            _context = context;
        }

        public IList<Models.Item> Item { get;set; } = default!;

        public async Task OnGetAsync(int? id)
        {
            Console.WriteLine("Nf Id: " + id);
            if (id != null)
            {
                Item = await _context.Items.Where(m => 
                    m.NotaItems.Any(n => n.NotaFiscalId == id)).ToListAsync();
            }
            else
                Item = await _context.Items.ToListAsync();
        }
    }
}
