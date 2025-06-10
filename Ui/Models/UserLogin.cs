using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace Ui.Models
{
    public class UserLogin
    {
        [Required(ErrorMessage = "Please to Enter FirstName")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Please To Enter LastName")]
        public string LastName { get; set; }
        [EmailAddress(ErrorMessage = "Please To Enter Your Email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "please TO Enter Passowrd")]
        [PasswordPropertyText]
        public string Passwoerd { get; set; }
        public string ReturnUrl { get; set; }
    }
}
