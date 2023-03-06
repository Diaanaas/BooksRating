using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksRating.Models;

public partial class Book
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Поле обов'язкове")]
	[Display(Name = "Назва книги")]
	public string Title { get; set; } = null!;

	[Required(ErrorMessage = "Поле обов'язкове")]
	[Display(Name = "Рік видання")]
	public DateTime PublishedYear { get; set; }

	[Required(ErrorMessage = "Поле обов'язкове")]
	[Display(Name = "Опис")]
	public string Description { get; set; } = null!;

	[Display(Name = "Обкладинка")]
	public byte[]? Cover { get; set; }

    public virtual ICollection<BookAuthor> BookAuthors { get; } = new List<BookAuthor>();

    public virtual ICollection<BookGenre> BookGenres { get; } = new List<BookGenre>();

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();
}
