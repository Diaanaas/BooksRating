using System;
using System.Collections.Generic;

namespace BooksRating.Models;

public partial class Country
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public virtual ICollection<Author> Authors { get; } = new List<Author>();
}
