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
    public  class RefreshTokensDTOs : BaseDtos
    {
        public string Token { get; set; }               // التوكن الفعلي
        public string UserId { get; set; }
        public DateTime ExpiryDate { get; set; }
        public int CurrentState { get; set; } = 1; // 1 = Active, 2 = Inactive

    }
}
