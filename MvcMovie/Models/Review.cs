using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace MvcMovie.Models
{
    public class Review
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Rating is required")]
        public int Rating { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; }

        [Required(ErrorMessage = "A Movie is required")]
        public Movie Movie { get; set; }
    }
}
