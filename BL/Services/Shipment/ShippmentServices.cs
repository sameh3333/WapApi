using AutoMapper;
using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using DAL.Contracts;
using Domines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services.Shipment
{
    public class ShippmentServices : BaseServices<TbShippment, ShippmentDTOs>, IShippment
    {
        IUserReceiver _userReceiver;
        IUserSebder _userSebder;
        ITrackingNumberCreator _trackingNumberCreator;
        IRateCalculator _rateCalculator;
        IUnitOfWork _uot;

        public ShippmentServices(IGenericRepository<TbShippment> redo, IMapper mapper, IUserService userService,
            IUserReceiver userReceiver, IUserSebder userSebder, ITrackingNumberCreator trackingNumberCreator, IRateCalculator rateCalculator, IUnitOfWork uot) 
            : base(redo, mapper, userService )
        {
            _uot =uot;
            _userReceiver = userReceiver;
            _userSebder = userSebder;
            _trackingNumberCreator = trackingNumberCreator;
            _rateCalculator = rateCalculator;
        }

        public async Task Create(ShippmentDTOs dto)
        {
            try
            {
                await _uot.BeginTransactionAsync();
                //Create Tracking Number
                dto.TrackingNumber = _trackingNumberCreator.Create(dto);

                //Calculate Rate
                dto.ShippingRate = _rateCalculator.Calculate(dto);
                //Save Sender
                if (dto.SenderId == Guid.Empty)
                {
                    Guid gSenderId = Guid.Empty;
                    _userSebder.Add(dto.UserSender, out gSenderId);
                    dto.SenderId = gSenderId;

                }
                //Save Receiver
                if (dto.ReceiverId == Guid.Empty)
                {
                    Guid gReceiverId = Guid.Empty;
                    _userReceiver.Add(dto.UserReceiver, out gReceiverId);
                    dto.ReceiverId = gReceiverId;

                }
                //save Shippment
                this.Add(dto);
               await _uot.CommitAsync();
            }
            catch
            {
                await _uot.RollbackAsync();
            }
        
        }
    }
}
