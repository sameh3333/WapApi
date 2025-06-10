using BL.DTOs;
using Domines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts.Shipment
{

    public interface IShippment : IBaseSerices<TbShippment, ShippmentDTOs>
    {
        public Task Create(ShippmentDTOs dtos);
    }
}
