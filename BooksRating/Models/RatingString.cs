using DocumentFormat.OpenXml.Wordprocessing;
using System.ComponentModel.DataAnnotations;

namespace BooksRating.Models
{
    public class RatingString
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Поле обов'язкове")]
        [Display(Name = "Рейтинг")]
        public string Rating { get; set; } = null!;

        public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();

    }
}
