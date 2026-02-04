using Microsoft.AspNetCore.Mvc.Formatters;
using System.ComponentModel.DataAnnotations;

namespace Movies.ViewModels.Admin
{
    public class MovieFormeViewModel
    {
        public int Id { get; set; }
        [Required(ErrorMessage = "Title is required")]
        [Display(Name = "Title")]
        [StringLength(200, MinimumLength = 1, ErrorMessage = "Title must be between 1 and 200 characters")]
        public string Title { get; set; } = string.Empty;

        [Required(ErrorMessage = "Description is required")]
        [Display(Name = "Description")]
        [StringLength(2000, MinimumLength = 1, ErrorMessage = "Description must be between 1 and 2000 characters")]
        public string? Description { get; set; }

        [Required(ErrorMessage = "Year is required")]
        [Display(Name = "Year")]
        [Range(1800, 2100, ErrorMessage = "Year must be between 1800 and 2100")]
        public int? Year { get; set; }

        [Required(ErrorMessage = "Genre is required")]
        [Display(Name = "Genre")]
        [StringLength(100, MinimumLength = 1, ErrorMessage = "Genre must be between 1 and 100 characters")]
        public string? Genre { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        [Range(0.0, 10.0, ErrorMessage = "Rating must be between 0.0 and 10.0")]
        [Display(Name = "Rating")]
        public double? Rating { get; set; }

        [Required(ErrorMessage = "Type is required")]
        [Display(Name = "Type")]
        public MediaType Type { get; set; }

        [Display(Name = "Poster")]
        public IFormFile? PosterFile { get; set; }

        public string? PosterUrl { get; set; }



    }
}
