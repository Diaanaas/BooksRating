using System;
using System.Collections.Generic;

namespace BooksRating.Models;

public partial class Book
{
    public int Id { get; set; }

    public string Title { get; set; } = null!;

    public DateTime PublishedYear { get; set; }

    public string Description { get; set; } = null!;

    public byte[]? Cover { get; set; }

    public virtual ICollection<BookAuthor> BookAuthors { get; } = new List<BookAuthor>();

    public virtual ICollection<BookGenre> BookGenres { get; } = new List<BookGenre>();

    public virtual ICollection<Rating> Ratings { get; } = new List<Rating>();
}
