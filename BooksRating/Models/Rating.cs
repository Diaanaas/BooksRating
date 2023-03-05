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

    public int BookId { get; set; }

    public int ReaderId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Reader Reader { get; set; } = null!;
}
