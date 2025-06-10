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
    public class ShipingPackgingDtos : BaseDtos
    {
        public string? ShipingPackgingAname { get; set; }

        public string? ShipingPackgingEname { get; set; }
    }
}
