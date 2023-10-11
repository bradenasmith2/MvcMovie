using Microsoft.AspNetCore.Mvc;
using MvcMovie.DataAccess;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class MoviesController : Controller
    {

        private readonly MvcMovieContext _context;

        public MoviesController(MvcMovieContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var movies = _context.Movies;
            if (!ModelState.IsValid)
            {
                return NotFound();
            }
            else
            {
                return View(movies);
            }
        }

        public IActionResult New()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Index(Movie movie)
        {
            if (!ModelState.IsValid)
            {
                return View("New", movie);
            }
            else
            {
                _context.Movies.Add(movie);
                _context.SaveChanges();

                var newMovieId = movie.Id;

                return RedirectToAction("show", new { id = newMovieId });
            }
        }

        [Route("Movies/{id:int}")]
        public IActionResult Show(int? id)
        {
            if(id == null)
            {
                return NotFound();
            }
            else if(_context.Movies.Where(e => e.Id == id).Any())
            {
                var movie = _context.Movies.Find(id);
                return View(movie);
            }
            else
            {
                return NotFound();
            }
        }
    }
}