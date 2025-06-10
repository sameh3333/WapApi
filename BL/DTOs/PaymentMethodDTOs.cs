using AddResourcessF.Carrier;
using AddResourcessF.EditShippingType;
using AddResourcessF.PaymentMethod;
using BL.DTOs.BaseDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public  class PaymentMethodDTOs : BaseDtos
    {
        [Required(ErrorMessageResourceName = "EnterA", ErrorMessageResourceType = typeof(PaymentMethodS), AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(PaymentMethodS))]
        public string? MethdAname { get; set; }
        [Required(ErrorMessageResourceName = "EnterE", ErrorMessageResourceType = typeof(PaymentMethodS), AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(PaymentMethodS))]
        public string? MethodEname { get; set; }
        [Required(ErrorMessageResourceName = "Commissions", ErrorMessageResourceType = typeof(PaymentMethodS), AllowEmptyStrings = false)]
        [Range(0.5,100, ErrorMessageResourceName = "LenghtNameCommissin",ErrorMessageResourceType = typeof(PaymentMethodS))]
        public double? Commission { get; set; }

    }
}
