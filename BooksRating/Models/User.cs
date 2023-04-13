using Microsoft.AspNetCore.Identity;

namespace BooksRating.Models
{
    public class User : IdentityUser
    {
        public int Year { get; set; }
    }

}
