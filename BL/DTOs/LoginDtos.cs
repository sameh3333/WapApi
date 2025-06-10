using AddResourcessF.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AddResourcessF.Settings;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public  class LoginDtos
    {

        [Required(ErrorMessageResourceName = "RequiredFieldError", ErrorMessageResourceType = typeof(Seting))]
        [EmailAddress(ErrorMessageResourceName = "EmailInvalidError", ErrorMessageResourceType = typeof(Seting))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldError", ErrorMessageResourceType = typeof(Seting))]
        [MinLength(6, ErrorMessageResourceName = "PasswordLengthError", ErrorMessageResourceType = typeof(Seting))]
        public string Password { get; set; }
    }
}
