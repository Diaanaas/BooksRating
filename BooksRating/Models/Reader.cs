using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BooksRating.Models;

public partial class Reader
{
    public int Id { get; set; }

	[Required(ErrorMessage = "Поле обов'язкове")]
	[Display(Name = "Ім'я читача")]
	public string Name { get; set; } = null!;

	[Required(ErrorMessage = "Поле обов'язкове")]
	[Display(Name = "Електронна пошта")]
	public string Email { get; set; } = null!;

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();
}
