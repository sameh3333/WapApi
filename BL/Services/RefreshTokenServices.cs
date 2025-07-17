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
    public  class RefreshTokenServices : BaseServices<  TbRefreshTokens,RefreshTokensDTOs> ,IRefreshTokens
    {
        IGenericRepository<TbRefreshTokens> _redo;
        IMapper _mapper;

        public RefreshTokenServices(IGenericRepository<TbRefreshTokens> redo, IMapper mapper ,IUserService userService) : base(redo,mapper,userService)
        {

            _redo = redo;
            _mapper = mapper;
        }

   

        public async Task< bool> Refresh(RefreshTokensDTOs TokenDto)
        {
            var tokinList =await _redo.GetList(a => a.UserId == TokenDto.UserId && a.CurrentState==1);
            foreach (var dbTonken in tokinList) {
                _redo.ChangeStatus(dbTonken.Id, Guid.Parse(TokenDto.UserId), 2);  
            }
         var dbtoken = _mapper.Map<RefreshTokensDTOs, TbRefreshTokens>(TokenDto);

            _redo.Add(dbtoken);
            return true;     


        }
    }
}













