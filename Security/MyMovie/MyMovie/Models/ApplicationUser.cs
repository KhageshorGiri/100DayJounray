using Microsoft.AspNetCore.Identity;

namespace MyMovie.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? Name { get; set; }
    }
}
