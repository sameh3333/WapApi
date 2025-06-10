using BL.DTOs;
using Domines;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BL.Contracts
{
    public interface ICity : IBaseSerices< TbCity,CityDTOs>
    {


        List<CityDTOs> GetAllCities();
        List<CityDTOs> GetByIdCounty(Guid countyId);
                        
    
    
    }
}
