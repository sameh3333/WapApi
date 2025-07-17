
using System;
using System.Collections.Generic;

namespace Domines
{

    public partial class TbShipingPackging : BaseTable
    {

        public string? ShipingPackgingAname { get; set; }

        public string? ShipingPackgingEname { get; set; }




        public virtual ICollection<TbShippment> TbShippments { get; set; } = new List<TbShippment>();
    }
}