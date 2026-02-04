using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

//using Microsoft.AspNetCore.Mvc.Formatters;
using Movies.Data;
using Movies.Models;
using Movies.ViewModels.Admin;

namespace Movies.Controllers
{
    public class AdminController : Controller
    {
        private const string AdminLogin = "admin";
        private const string AdminPassword = "password";

        ApplicationDbContext db;
        public AdminController(ApplicationDbContext context)
        {
            db = context;
        }
        public async Task<IActionResult> Index()
        {
            var movies = await db.Movies.ToListAsync();
            return View(movies);
        }

        // Гет запиту для відображення форми входу
        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        // Пост запиту для обробки форми входу
        [HttpPost]
        public IActionResult Login(string login, string password)
        {
            // Перевірка логіна та пароля (жорстко закодовані для прикладу)
            if (login == AdminLogin && password == AdminPassword)
            {
                // Успішний вхід
                return RedirectToAction("Create", "Admin");
            }
            else
            {
                // Невдалий вхід
                ModelState.AddModelError("", "Невірний логін або пароль");
                return View();
            }

        }

        // Гет запиту для відображення списку фільмів
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        // Пост запиту для створення нового фільму
        [HttpPost]
        public IActionResult Create(string title, string description, string posterUrl, int year, string genre, double rating)
        {
            // Створення нового фільму
            var movie = new Movie
            {
                Title = title,
                Description = description,
                PosterUrl = posterUrl,
                Year = year,
                Genre = genre,
                Rating = rating,
                CreatedAt = DateTime.Now
            };
            db.Movies.Add(movie);
            db.SaveChanges();
            return RedirectToAction("Index", "Admin");
        }

        // Пост запиту для видалення фільму
        [HttpPost]
        public IActionResult Delete(int id)
        {
            var movie = db.Movies.Find(id);
            if (movie != null)
            {
                db.Movies.Remove(movie);
                db.SaveChanges();
            }
            return RedirectToAction("Movies");
        }

        [HttpPost]
        public IActionResult Logout()
        {
            // Логіка виходу (якщо використовується аутентифікація)
            return RedirectToAction("Login", "Admin");
        }
    }
}
