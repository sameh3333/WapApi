using BL.DTOs.BaseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AddResourcessF.Settings;

using System.ComponentModel.DataAnnotations;
namespace BL.DTOs
{
    public class SettingDTOs : BaseDtos
    {
        [Required(ErrorMessageResourceName = "KiloMeterRateVaildtion", ErrorMessageResourceType = typeof(Seting), AllowEmptyStrings = false)]
        [Range(0.5, 100000, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(Seting))]
        public double? KiloMeterRate { get; set; }
        [Required(ErrorMessageResourceName = "KilooGramRateVaildtion", ErrorMessageResourceType = typeof(Seting), AllowEmptyStrings = false)]
        [Range(0.5, 100000, ErrorMessageResourceName = "LenghtName", ErrorMessageResourceType = typeof(Seting))] 
        public double? KilooGramRate { get; set; }
    }
}
