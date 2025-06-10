using AddResourcessF.Countrys
;
using AddResourcessF.EditShippingType;
using BL.DTOs.BaseDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CountryDTOs : BaseDtos
    {
        [Required(ErrorMessageResourceName = "EnterA", ErrorMessageResourceType = typeof(Edit), AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 5, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(Edit))]
        public string? CountryAname { get; set; }
        [Required(ErrorMessageResourceName = "EnterE", ErrorMessageResourceType = typeof(Edit), AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 5, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(Edit))]
        public string? CountryEname { get; set; }
    }
}
