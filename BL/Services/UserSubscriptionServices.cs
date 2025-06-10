using AutoMapper;
using BL.Contracts;
using BL.DTOs;
using DAL.Contracts;
using Domines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Services
{
    public class UserSubscriptionServices : BaseServices<TbUserSubscription, UserSubscriptionDTOs>, IUserSubscription
    {
        public UserSubscriptionServices(IGenericRepository<TbUserSubscription> redo, IMapper mapper, IUserService userService) : base(redo, mapper, userService)
        {
        }
    }
}
