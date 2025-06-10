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
    public class UserSebderSerivces : BaseServices<TbUserSender, UserSenderDTOs>, IUserSebder
    {
        IUnitOfWork _uot;

        public UserSebderSerivces(IGenericRepository<TbUserSender> redo, IMapper mapper, IUserService userService, IUnitOfWork uot) : base(uot, mapper, userService)
        {
            _uot = uot;
        }

    }
}
