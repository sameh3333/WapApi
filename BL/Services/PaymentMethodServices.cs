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
    public class PaymentMethodServices : BaseServices<TbPaymentMethod, PaymentMethodDTOs>, IPaymentMethod
    {
        public PaymentMethodServices(IGenericRepository<TbPaymentMethod> redo, IMapper mapper, IUserService userService) : base(redo, mapper,userService    )
        {
        }
    }
}
