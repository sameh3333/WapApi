using BL.DTOs.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
using AddResourcessF.EditShippingType;

namespace BL.DTOs
{
    public class ShippingTypeDTOs : BaseDtos
    {
        [Required(ErrorMessageResourceName = "Required_ShippingTypeAname", ErrorMessageResourceType = typeof(EditA), AllowEmptyStrings = false)]
        //   [StringLength(ErrorMessageResourceName = "LenghtName",ErrorMessageResourceType ==typeof(EditA), AllowEmptyStrings=false)]   
        [StringLength(100, MinimumLength = 5, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(EditA))]
        public string? ShippingTypeAname { get; set; }
        [Required(ErrorMessageResourceName = "Required_ShippingTypeEname", ErrorMessageResourceType = typeof(EditN), AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 5, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(EditN))]
        public string? ShippingTypeEname { get; set; }
        [Required(ErrorMessageResourceName = "ShippingFactor", ErrorMessageResourceType =typeof(EditN))]
        [Range(10, 0.25, ErrorMessageResourceName = "Range_ShippingFactor",ErrorMessageResourceType =typeof(EditN))]
        public double ShippingFactor { get; set; }
    }
}
