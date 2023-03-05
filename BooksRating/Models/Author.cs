using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksRating.Models;

public partial class Author
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Поле обов'язкове")]
	[Display(Name = "Ім'я автора")]
	public string Name { get; set; } = null!;

    public int CountryId { get; set; }

    public virtual ICollection<BookAuthor> BookAuthors { get; } = new List<BookAuthor>();

    public virtual Country Country { get; set; } = null!;
}
