using BL.Contracts.Shipment;
using BL.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Shipment
{
    public class TrackingNumberCretatorServices : ITrackingNumberCreator
    {
        public double Create(ShippmentDTOs dtos)
        {
            return 45545.00;
        }
    }
}
