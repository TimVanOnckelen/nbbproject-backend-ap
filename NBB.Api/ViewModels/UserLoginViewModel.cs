using System.ComponentModel.DataAnnotations;

namespace NBB.Api.ViewModels
{
    public class UserLoginViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Password { get; set; }
    }
}
