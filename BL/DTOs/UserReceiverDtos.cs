﻿using BL.DTOs.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public  class UserReceiverDtos : BaseDtos
    {
        public Guid UserId { get; set; }

        public string ReceiverName { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string Phone { get; set; } = null!;
        public string PostalCode { get; set; }
        public string Contact { get; set; } = null!;
        public bool IsDefalte { get; set; }

        public string OtherAddress { get; set; } = null!;
        public Guid CityId { get; set; }

        public string Address { get; set; } = null!;


    }
}
