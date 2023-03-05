using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksRating.Models;

public partial class Country
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Поле обов'язкове")]
	[Display(Name = "Назва країни")]
	public string Name { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; } = new List<Author>();
}
