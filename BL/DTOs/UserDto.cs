using BL.DTOs.BaseDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AddResourcessF.Settings;
using System.Text;
using System.Threading.Tasks;
using AddResourcessF.PaymentMethod;

namespace BL.DTOs
{
    public  class UserDto : BaseDtos
    {


        [Required(ErrorMessageResourceName = "RequiredFieldError", ErrorMessageResourceType = typeof(Seting))]
        [EmailAddress(ErrorMessageResourceName = "EmailInvalidError", ErrorMessageResourceType = typeof(Seting))]
        public string Email { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldError", ErrorMessageResourceType = typeof(Seting))]
        [StringLength(5, MinimumLength = 2, ErrorMessageResourceName = "LengthRequiredFieldError", ErrorMessageResourceType = typeof(Seting))]
        public string FirstName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldError", ErrorMessageResourceType = typeof(Seting))]
        [StringLength(5, MinimumLength = 2, ErrorMessageResourceName = "LengthRequiredFieldError", ErrorMessageResourceType = typeof(Seting))]
        public string LastName { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldError", ErrorMessageResourceType = typeof(Seting))]
        [Range(10000000, 9999999999, ErrorMessageResourceName = "PhoneNumberInvalidError", ErrorMessageResourceType = typeof(Seting))]
        public string Phone { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldError", ErrorMessageResourceType = typeof(Seting))]
        [MinLength(6, ErrorMessageResourceName = "PasswordLengthError", ErrorMessageResourceType = typeof(Seting))]
        public string Password { get; set; }

        [Required(ErrorMessageResourceName = "RequiredFieldError", ErrorMessageResourceType = typeof(Seting))]
        //[Compare("Password", ErrorMessageResourceName = "PasswordMismatchError", ErrorMessageResourceType = typeof(Seting))]
        public string? ConfirmPassword { get; set; }

        public string? Role { get; set; }

        public string? ReturnUrl { get; set; }

    }
}
