using System;
using System.Collections.Generic;

namespace BooksRating.Models;

public partial class Rating
{
    public int Id { get; set; }

    public double Rating1 { get; set; }

    public int BookId { get; set; }

    public int ReaderId { get; set; }

    public virtual Book Book { get; set; } = null!;

    public virtual Reader Reader { get; set; } = null!;
}
