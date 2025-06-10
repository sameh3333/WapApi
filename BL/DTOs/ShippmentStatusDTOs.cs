using BL.DTOs.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public class ShippmentStatusDTOs : BaseDtos
    {
        public Guid? ShippmentId { get; set; }


        public string? Notes { get; set; }

        public Guid CarrierId { get; set; }
    }
}
