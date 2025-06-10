using BL.DTOs;
using Domines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public  interface IRefreshTokens : IBaseSerices<TbRefreshTokens,RefreshTokensDTOs>
    {
       public  RefreshTokensDTOs GetByToken(string Tokin);
        public bool Refresh(RefreshTokensDTOs TokenDto);






    }
}
