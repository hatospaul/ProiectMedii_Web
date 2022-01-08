using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Data;
using Web.Models;

namespace Web.Pages.Movies
{
    public class CreateModel : MovieGenresPageModel
    {
        private readonly Web.Data.WebContext _context;

        public CreateModel(Web.Data.WebContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            ViewData["PublisherID"] = new SelectList(_context.Set<Publisher>(), "ID", "PublisherName");

            var movie = new Movie();
            movie.MovieGenres = new List<MovieGenre>();

            PopulateAssignedGenreData(_context, movie);

            return Page();
        }

        [BindProperty]
        public Movie Movie { get; set; }

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync(string[] selectedGenres)
        {
            var newMovie = new Movie();
            if (selectedGenres != null)
            {
                newMovie.MovieGenres = new List<MovieGenre>();
                foreach (var cat in selectedGenres)
                {
                    var catToAdd = new MovieGenre
                    {
                        GenreID = int.Parse(cat)
                    };
                    newMovie.MovieGenres.Add(catToAdd);
                }
            }
            if (await TryUpdateModelAsync<Movie>(
            newMovie,
            "Movie",
            i => i.Title, i => i.Director,
            i => i.Price, i => i.PublishingDate, i => i.PublisherID))
            {
                _context.Movie.Add(newMovie);
                await _context.SaveChangesAsync();
                return RedirectToPage("./Index");
            }
            PopulateAssignedGenreData(_context, newMovie);
            return Page();
        }
    }
}
