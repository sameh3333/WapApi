using AutoMapper;
using BL.DTOs;
using Domines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Maping
{
    public  class MappingProfile :Profile
    {
        public MappingProfile() 
        {
            CreateMap<TbCarrier, CarrierDtos>().ReverseMap();
            CreateMap<TbCity,CityDTOs>().ReverseMap();
            CreateMap<VwCities, CityDTOs>().ReverseMap();
            CreateMap<TbCountry,CountryDTOs>(). ReverseMap();
            CreateMap<TbPaymentMethod,PaymentMethodDTOs>().ReverseMap();
            CreateMap<TbSetting,SettingDTOs>().ReverseMap();
            CreateMap<TbShippingType,ShippingTypeDTOs>().ReverseMap();  
            CreateMap<TbShippment, ShippmentDTOs>().ReverseMap();    
            CreateMap<TbShippmentStatus, ShippmentStatusDTOs>().ReverseMap();
            CreateMap<TbSubscriptionPackage, SubscriptionPackageDTOs>().ReverseMap();   
            CreateMap<TbUserSender,UserSenderDTOs>().ReverseMap();
            CreateMap<TbUserReceiver,UserReceiverDtos>().ReverseMap();
            CreateMap<TbUserSubscription, UserSubscriptionDTOs>().ReverseMap();  
            CreateMap<TbRefreshTokens, RefreshTokensDTOs>().ReverseMap();
        }
    }
}
