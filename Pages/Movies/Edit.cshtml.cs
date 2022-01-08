using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Web.Data;
using Web.Models;

namespace Web.Pages.Movies
{
    public class EditModel : MovieGenresPageModel
    {
        private readonly Web.Data.WebContext _context;

        public EditModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Movie Movie { get; set; }

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Movie = await _context.Movie.Include(b => b.Publisher).Include(b => b.MovieGenres).ThenInclude(b => b.Genre).AsNoTracking().FirstOrDefaultAsync(m => m.ID == id);

            if (Movie == null)
            {
                return NotFound();
            }

            PopulateAssignedGenreData(_context, Movie);

            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");
            return Page();
        }

        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see https://aka.ms/RazorPagesCRUD.
        public async Task<IActionResult> OnPostAsync(int? id, string[] selectedGenres)
        {
            if (id == null)
            {
                return NotFound();
            }
            var movieToUpdate = await _context.Movie
            .Include(i => i.Publisher)
            .Include(i => i.MovieGenres)
            .ThenInclude(i => i.Genre)
            .FirstOrDefaultAsync(s => s.ID == id);
            if (movieToUpdate == null)
            {
                return NotFound();
            }
            if (await TryUpdateModelAsync<Movie>(
            movieToUpdate,
            "Movie",
            i => i.Title, i => i.Director,
            i => i.Price, i => i.PublishingDate, i => i.Publisher))
            {
                UpdateMovieGenres(_context, selectedGenres, movieToUpdate);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            //Apelam UpdateBookCategories pentru a aplica informatiile din checkboxuri la entitatea Books care
            //este editata
            UpdateMovieGenres(_context, selectedGenres, movieToUpdate);
            PopulateAssignedGenreData(_context, movieToUpdate);
            return Page();
        }
    
    }
}
