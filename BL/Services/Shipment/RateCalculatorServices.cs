using BL.Contracts.Shipment;
using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Shipment
{
    public class RateCalculatorServices : IRateCalculator
    {
        public decimal Calculate(ShippmentDTOs DTOS)
        {
            return 9999;
        }
    }
}
