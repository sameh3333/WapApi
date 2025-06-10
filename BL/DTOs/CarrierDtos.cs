using AddResourcessF.Carrier;
using BL.DTOs.BaseDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class CarrierDtos : BaseDtos
    {
        [Required(ErrorMessageResourceName = "EnterA", ErrorMessageResourceType = typeof(ListCarrir), AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(ListCarrir))]
        public string CarrierName { get; set; } = null!;

    }
}
