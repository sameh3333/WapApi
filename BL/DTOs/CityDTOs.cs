using AddResourcessF.City;
using AddResourcessF.Countrys;
using BL.DTOs.BaseDto;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using AddResourcessF.EditShippingType;
using System.Linq;
using System.Text;
using AddResourcessF.City;
using System.Threading.Tasks;

namespace BL.DTOs
{
    public  class CityDTOs : BaseDtos
    {
        [Required(ErrorMessageResourceName = "RequiredCityAname", ErrorMessageResourceType = typeof(AddResourcessF.City.Edit), AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(EditA))]
        public string? CityAname { get; set; }
        [Required(ErrorMessageResourceName = "RequiredCityEname", ErrorMessageResourceType = typeof(AddResourcessF.City.Edit), AllowEmptyStrings = false)]
        [StringLength(100, MinimumLength = 3, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(EditA))]
        public string? CityEname { get; set; }
        public string? CountryAname { get; set; }
        public string? CountryEname { get; set; }
        [Required(ErrorMessageResourceName = "EnterE", ErrorMessageResourceType = typeof(AddResourcessF.Countrys.Edit), AllowEmptyStrings = false)]
        public Guid CountryId { get; set; }

    }
}
