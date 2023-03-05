using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksRating.Models;

public partial class Genre
{
    public int Id { get; set; }

    [Required(ErrorMessage ="Поле обов'язкове")]
    [Display(Name = "Жанр")]
    public string Name { get; set; } = null!;

    public virtual ICollection<BookGenre> BookGenres { get; } = new List<BookGenre>();
}
