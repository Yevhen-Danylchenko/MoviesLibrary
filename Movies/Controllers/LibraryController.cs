using Microsoft.AspNetCore.Mvc;
using Movies.Data;
using Movies.Models;
using Movies.ViewModels.Library;

namespace Movies.Controllers
{
    public class LibraryController : Controller
    {
        // Контекст бази даних
        private readonly ApplicationDbContext db;

        public LibraryController(ApplicationDbContext context)
        {
            db = context;
        }

        // Перегляд бібліотеки з фільтрами
        public IActionResult Index(WatchStatus? status, MediaType? type, string? search)
        {
            // Базовий запит
            var query = db.UserMovies.AsQueryable();

            // Застосування фільтрів
            if (status.HasValue)
                query = query.Where(x => x.Status == status.Value);

            if (type.HasValue)
                query = query.Where(x => x.Movie.Type == type.Value);

            if (!string.IsNullOrEmpty(search))
                query = query.Where(x => x.Movie.Title.Contains(search));

            // Створення моделі представлення
            var model = new LibraryViewModel
            {
                CurrentFilter = status,
                TypeFilter = type,
                SearchQuery = search,
                Items = query.Select(x => new UserMovieItem
                {
                    UserMovieId = x.Id,
                    MovieId = x.MovieId,
                    Title = x.Movie.Title,
                    PosterUrl = x.Movie.PosterUrl,
                    Year = x.Movie.Year,
                    Type = x.Movie.Type,
                    Status = x.Status,
                    UserRating = x.UserRating,
                    AddedAt = x.AddedAt
                }).ToList()
            };

            return View(model);
        }

        // GET: додати фільм у бібліотеку
        public IActionResult Add(int movieId)
        {
            // Пошук фільму
            var movie = db.Movies.Find(movieId);
            if (movie == null) return NotFound();

            // Створення моделі представлення
            var model = new AddToLibraryViewModel
            {
                MovieId = movie.Id,
                Movie = movie
            };

            return View(model);
        }

        // POST: додати фільм у бібліотеку
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Add(AddToLibraryViewModel model)
        {
            // Перевірка валідності моделі
            if (!ModelState.IsValid)
                return View(model);

            // Створення запису у бібліотеці
            var entity = new UserMovie
            {
                MovieId = model.MovieId,
                Status = model.Status,
                UserRating = model.UserRating,
                Notes = model.Notes,
                AddedAt = DateTime.UtcNow
            };

            db.UserMovies.Add(entity);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // Редагування статусу/оцінки
        public IActionResult Edit(int id)
        {
            // Пошук запису у бібліотеці
            var userMovie = db.UserMovies.Find(id);
            if (userMovie == null) return NotFound();

            // Створення моделі представлення
            var model = new AddToLibraryViewModel
            {
                MovieId = userMovie.MovieId,
                Status = userMovie.Status,
                UserRating = userMovie.UserRating,
                Notes = userMovie.Notes,
                Movie = db.Movies.Find(userMovie.MovieId)
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, AddToLibraryViewModel model)
        {
            if (!ModelState.IsValid)
                return View(model);

            // Пошук запису у бібліотеці
            var userMovie = db.UserMovies.Find(id);
            if (userMovie == null) return NotFound();

            // Оновлення полів
            userMovie.Status = model.Status;
            userMovie.UserRating = model.UserRating;
            userMovie.Notes = model.Notes;

            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // Видалення
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            // Пошук запису у бібліотеці
            var userMovie = db.UserMovies.Find(id);
            if (userMovie == null) return NotFound();

            db.UserMovies.Remove(userMovie);
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }

        // GET: оновити статус (форма)
        public IActionResult UpdateStatus(int id)
        {
            // Пошук запису у бібліотеці
            var userMovie = db.UserMovies.Find(id);
            if (userMovie == null) return NotFound();

            // Створення моделі представлення
            var model = new AddToLibraryViewModel
            {
                MovieId = userMovie.MovieId,
                Status = userMovie.Status,
                UserRating = userMovie.UserRating,
                Notes = userMovie.Notes,
                Movie = db.Movies.Find(userMovie.MovieId)
            };

            return View(model);
        }

        // POST: оновити статус
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult UpdateStatus(int id, WatchStatus status)
        {
            // Пошук запису у бібліотеці
            var userMovie = db.UserMovies.Find(id);
            if (userMovie == null) return NotFound();

            // Оновлення статусу
            userMovie.Status = status;
            db.SaveChanges();

            return RedirectToAction(nameof(Index));
        }
    }
}
