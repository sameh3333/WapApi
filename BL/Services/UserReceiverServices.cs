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
    public class UserReceiverServices : BaseServices<TbUserReceiver, UserReceiverDtos>, IUserReceiver
    {
        IUnitOfWork _uot;
        public UserReceiverServices(IGenericRepository<TbUserReceiver> redo, IMapper mapper, IUserService userService, IUnitOfWork uot) : base(uot, mapper, userService)
        {
            _uot = uot;
        }
    }
}
