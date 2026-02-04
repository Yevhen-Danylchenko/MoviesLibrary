using Movies.Models;
using System.ComponentModel.DataAnnotations;

namespace Movies.ViewModels.Library
{
    public class AddToLibraryViewModel
    {
        public int MovieId { get; set; }

        [Display(Name = "Статус")]
        public WatchStatus Status { get; set; }

        [Display(Name = "Оцінка 1-10")]
        [Range(1, 10, ErrorMessage = "Оцінка від 1 до 10")]
        public int? UserRating { get; set; }

        [Display(Name = "Нотатки")]
        [StringLength(500)]
        public string? Notes { get; set; }

        public Movie? Movie { get; set; }
    }
}
