using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Movies.Data;
using Movies.Models;
using Movies.ViewModels.Profile;

namespace Movies.Controllers
{
    public class ProfileController : Controller
    {
        // Підключення до бази даних через контекст
        private readonly ApplicationDbContext db;

        public ProfileController(ApplicationDbContext context)
        {
            db = context;
        }

        public IActionResult Index()
        {
            // Отримуємо поточного користувача (припустимо, що є аутентифікація)
            var userEmail = User.Identity?.Name;
            if (string.IsNullOrEmpty(userEmail))
                return RedirectToAction("Login", "Account");

            // Завантажуємо користувача з бази даних разом з його фільмами
            var user = db.Users
                .Include(u => u.UserMovies)
                .ThenInclude(um => um.Movie)
                .FirstOrDefault(u => u.Email == userEmail);

            if (user == null)
                return NotFound();

            // Підрахунок статистики
            var totalMovies = user.UserMovies.Count(um => um.Movie.Type == MediaType.Movie);
            var totalSeries = user.UserMovies.Count(um => um.Movie.Type == MediaType.Series);

            // Підрахунок за статусами перегляду
            var watching = user.UserMovies.Count(um => um.Status == WatchStatus.Watching);
            var completed = user.UserMovies.Count(um => um.Status == WatchStatus.Completed);
            var planToWatch = user.UserMovies.Count(um => um.Status == WatchStatus.PlanToWatch);

            // Створення моделі для представлення
            var model = new ProfileViewModel
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                RegisterAt = user.RegisteredAt,
                TotalMovies = totalMovies,
                TotalSeries = totalSeries,
                Watching = watching,
                Completed = completed,
                PlanToWatch = planToWatch
            };

            return View(model);
        }

        // Редагування профілю
        public IActionResult Edit()
        {
            // Отримуємо поточного користувача
            var userEmail = User.Identity?.Name;
            var user = db.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null) return NotFound();

            // Заповнюємо модель для редагування
            var model = new ProfileViewModel
            {
                DisplayName = user.DisplayName,
                Email = user.Email,
                AvatarUrl = user.AvatarUrl,
                RegisterAt = user.RegisteredAt
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(ProfileViewModel model)
        {
            // Отримуємо поточного користувача
            var userEmail = User.Identity?.Name;
            var user = db.Users.FirstOrDefault(u => u.Email == userEmail);
            if (user == null) return NotFound();

            // Перевірка валідності моделі
            if (!ModelState.IsValid)
                return View(model);

            // Оновлення інформації користувача
            user.DisplayName = model.DisplayName;
            user.AvatarUrl = model.AvatarUrl;

            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
