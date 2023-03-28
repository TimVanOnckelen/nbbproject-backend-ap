using Microsoft.AspNetCore.Identity;

namespace NBB.Api.Models
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
    }

}
