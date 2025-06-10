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
    public  class CityServices :BaseServices<TbCity,CityDTOs>, ICity
    {
        IViewRepository<VwCities> _ViewRepo;
        IMapper _imapper;
       public CityServices(IGenericRepository<TbCity> redo,IMapper imapper, IUserService userService, IViewRepository<VwCities> viewRepo) :base(redo,imapper,userService)
        {
        _ViewRepo = viewRepo;
            _imapper = imapper;
        }
        
        public List<CityDTOs> GetAllCities()
        {
            var Citeies = _ViewRepo.GetList(a => a.CurrentState == 1).ToList();
            return _imapper.Map<List<VwCities>, List<CityDTOs>>(Citeies);
        }

        public List<CityDTOs> GetByIdCounty(Guid countyId)
        {
            var Citeies = _ViewRepo.GetList(a => a.CurrentState == 1&& a.CountryId==countyId).ToList();

            return _imapper.Map<List<VwCities>, List<CityDTOs>>(Citeies);
        }
    }
}
