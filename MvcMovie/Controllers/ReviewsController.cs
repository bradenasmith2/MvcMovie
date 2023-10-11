using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MvcMovie.DataAccess;
using MvcMovie.Models;

namespace MvcMovie.Controllers
{
    public class ReviewsController : Controller
    {
        private readonly MvcMovieContext _context;

        public ReviewsController(MvcMovieContext context)
        {
            _context = context;
        }

        // GET: /movies/:movieId/reviews
        [Route("Movies/{movieId:int}/reviews")]
        public IActionResult Index(int? movieId)
        {
            if(movieId == null)
            {
                return NotFound();
            }
            else if(_context.Reviews.Where(e => e.Id == movieId).Any())
            {
                var movie = _context.Movies
                .Where(m => m.Id == movieId)
                .Include(m => m.Reviews)
                .First();

                return View(movie);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("/movies/{movieId:int}/review/new")]
        public IActionResult New(int movieId)
        {
            if(movieId == null)
            {
                return NotFound();
            }
            else
            {
                ViewData["MovieId"] = movieId;
                return View();
            }
        }

        [HttpPost]
        [Route("/movies/{movieId:int}/review/create")]
        public IActionResult Index(int movieId, Review review)
        {
            if(movieId == null)
            {
                return NotFound();
            }

            else if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                _context.SaveChanges();

                return Redirect($"/movies/{movieId}/reviews");
            }
            else
            {
                return NotFound();
            }
        }
    }
}
