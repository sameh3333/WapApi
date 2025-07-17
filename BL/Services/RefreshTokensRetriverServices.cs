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
    public  class RefreshTokensRetriverServices  :IRefreshTokensRetriver
    {
        IGenericRepository<TbRefreshTokens> _redo;
        IMapper _mapper;

        public RefreshTokensRetriverServices(IGenericRepository<TbRefreshTokens> redo, IMapper mapper )
        {

            _redo = redo;
            _mapper = mapper;
        }

        public RefreshTokensDTOs GetByToken(string Tokin)
        {
            var refreshToken = _redo.GetFirstOrDefault(x => x.Token == Tokin);
            return _mapper.Map<TbRefreshTokens, RefreshTokensDTOs>(refreshToken);
        }

    }
}













