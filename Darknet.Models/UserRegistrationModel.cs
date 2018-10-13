using System.ComponentModel.DataAnnotations;

namespace Darknet.Models
{
    public class UserRegistrationModel
    {
        [Required]
        [Display(Name = "First Name")]
        public string FirstName { get; set; }
        [Required]
        [Display(Name = "Last Name")]
        public string LastName { get; set; }
        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }

    public class UserCredentials
    {
        [Required]
        [Display(Name = "User Name")]
        public string Username { get; set; }
        [Required]
        [Display(Name = "Password")]
        public string Password { get; set; }
    }
}
