﻿using BL.DTOs.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public  class ShippmentDTOs : BaseDtos
    {
        public DateTime ShippingDate { get; set; }
        public DateTime DelivryDate { get; set; }

        public UserSenderDTOs UserSender { get; set; }
        public Guid SenderId { get; set; }

        public UserReceiverDtos UserReceiver { get; set; }
        public Guid ReceiverId { get; set; }

        public Guid ShippingTypeId { get; set; }
        public Guid? ShipingPackgingId { get; set; }

        public double Width { get; set; }

        public double Height { get; set; }

        public double Weight { get; set; }

        public double Length { get; set; }

        public decimal PackageValue { get; set; }

        public decimal ShippingRate { get; set; }

        public Guid? PaymentMethodId { get; set; }

        public Guid? UserSubscriptionId { get; set; }

        public double? TrackingNumber { get; set; }

        public Guid? ReferenceId { get; set; }

    }
}
