using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Movies.Models;

namespace Movies.Controllers
{
    public class MovieController : Controller
    {
        // Контекст бази даних
        private readonly ApplicationDbContext db;

        public MovieController(ApplicationDbContext context)
        {
            db = context;
        }

        // Список фільмів
        public IActionResult Index()
        {
            // Отримання списку фільмів з бази даних
            var movies = db.Movies.ToList();
            return View(movies);
        }

        // Деталі фільму
        public IActionResult Details(int id)
        {
            var movie = db.Movies.Find(id);
            if (movie == null) return NotFound();

            return View(movie);
        }
    }
}
