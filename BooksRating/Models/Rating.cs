using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksRating.Models;

public partial class Rating
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Поле обов'язкове")]
    [Display(Name = "Рейтинг книги")]
    public double Rating1 { get; set; }

    [Display(Name = "Книга")]
    public int BookId { get; set; }

    [Display(Name = "Читач")]
    public int ReaderId { get; set; }

    [Display(Name = "Книга")]
    public virtual Book Book { get; set; } = null!;

    [Display(Name = "Читач")]
    public virtual Reader Reader { get; set; } = null!;
}
