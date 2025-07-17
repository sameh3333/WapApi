using AutoMapper;
using BL.Contracts;
using BL.Contracts.Shipment;
using BL.DTOs;
using DAL.Contracts;
using DAL.Models;
using Domines;
using Microsoft.EntityFrameworkCore;
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
        IUserService _userService;
        IMapper _imapper;
        ShippingContext _context;
        IGenericRepository<TbShippment> _redo;

        public ShippmentServices(IGenericRepository<TbShippment> redo, IMapper mapper, IUserService userService,
            IUserReceiver userReceiver, IUserSebder userSebder, ITrackingNumberCreator trackingNumberCreator, IRateCalculator rateCalculator,
            IUnitOfWork uot, IMapper mappr, ShippingContext context ) 
            : base(redo, mapper, userService  )
        {
            _redo= redo;
            _uot =uot;
            _userReceiver = userReceiver;
            _userSebder = userSebder;
            _trackingNumberCreator = trackingNumberCreator;
            _rateCalculator = rateCalculator;
            _userService=userService;
            _imapper=mapper;    
            _context=context;   
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
                var userId = _userService.GetLoggedInServices();
                if (dto.SenderId == Guid.Empty)
                {
                    Guid gSenderId = Guid.Empty;
                    dto.UserSender.UserId = userId;
                    _userSebder.Add(dto.UserSender, out gSenderId);
                    dto.SenderId = gSenderId;

                }
                //Save Receiver
                if (dto.ReceiverId == Guid.Empty)
                {
                    Guid gReceiverId = Guid.Empty;
                    dto.UserReceiver.UserId = userId;
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
                throw new Exception();

            }

        }

        public async Task<List<ShippmentDTOs>> GetShipments()
        {
            try
            {
                var userId= _userService.GetLoggedInServices();
                var shippment = await _redo.GetList(a => a.CreatedBy == userId);

                return _imapper.Map<List<TbShippment>,List<ShippmentDTOs>>(shippment);

            }
            catch (Exception ex) {
                throw new Exception();
            }
           
        }
    }
}
