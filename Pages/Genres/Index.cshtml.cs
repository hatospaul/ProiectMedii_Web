using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Pages.Genres
{
    public class IndexModel : PageModel
    {
        private readonly Web.Data.WebContext _context;

        public IndexModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        public IList<Genre> Genre { get;set; }

        public async Task OnGetAsync()
        {
            Genre = await _context.Genre.ToListAsync();
        }
    }
}
