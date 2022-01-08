using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Web.Data;

namespace Web.Models
{
    public class MovieGenresPageModel:PageModel
    {
        public List<AssignedGenreData> AssignedGenreDataList;

        public void PopulateAssignedGenreData(WebContext context, Movie movie)

        {
            var allGenres = context.Genre;
            var movieGenres = new HashSet<int>(
            movie.MovieGenres.Select(c => c.GenreID)); //
            AssignedGenreDataList = new List<AssignedGenreData>();
            foreach (var cat in allGenres)
            {
                AssignedGenreDataList.Add(new AssignedGenreData
                {
                    GenreID = cat.ID,
                    Name = cat.GenreName,
                    Assigned = movieGenres.Contains(cat.ID)
                });
            }
        }

        public void UpdateMovieGenres(WebContext context, string[] selectedGenres, Movie movieToUpdate)
        {
            if (selectedGenres == null)
            {
                movieToUpdate.MovieGenres = new List<MovieGenre>();
                return;
            }
            var selectedGenresHS = new HashSet<string>(selectedGenres);
            var movieGenres = new HashSet<int>
            (movieToUpdate.MovieGenres.Select(c => c.Genre.ID));
            foreach (var cat in context.Genre)
            {
                if (selectedGenresHS.Contains(cat.ID.ToString()))
                {
                    if (!movieGenres.Contains(cat.ID))
                    {
                        movieToUpdate.MovieGenres.Add(
                        new MovieGenre
                        {
                            MovieID = movieToUpdate.ID,
                            GenreID = cat.ID
                        });
                    }
                }
                else
                {
                    if (movieGenres.Contains(cat.ID))
                    {
                        MovieGenre courseToRemove
                        = movieToUpdate
                        .MovieGenres
                       .SingleOrDefault(i => i.GenreID == cat.ID);
                        context.Remove(courseToRemove);
                    }
                }
            }
        }
    }
}
