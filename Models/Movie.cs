using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Web.Models
{
    public class Movie
    {
        public int ID { get; set; }

        [Required, StringLength(150, MinimumLength = 3)]

        [Display(Name = "Movie Title")]
        public string Title { get; set; }

        [RegularExpression(@"^[A-Z][a-z]+\s[A-Z][a-z]+$", ErrorMessage = "Numele directorului nu este valid!"), Required, StringLength(50, MinimumLength = 3)]
        public string Director { get; set; }

        [Range(1, 300)]

        [Column(TypeName = "decimal(6, 2)")]

        [Display(Name = "Rental Price")]
        public decimal Price { get; set; }

        [DataType(DataType.Date)]

        [Display(Name = "Release Date")]
        public DateTime PublishingDate { get; set; }
        public int PublisherID { get; set; }

        [Display(Name = "Publisher")]
        public Publisher Publisher { get; set; }

        [Display(Name = "Genres")]
        public ICollection<MovieGenre> MovieGenres { get; set; }
    }
}
