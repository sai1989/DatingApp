using System.ComponentModel.DataAnnotations;

namespace DatingApp.API.Dtos
{
    public class UserForRegistrationDto
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        [StringLength(20,MinimumLength=8,ErrorMessage="Password should be greater than 8 characters and less than 20")]
        public string Password { get; set; }
    }
}