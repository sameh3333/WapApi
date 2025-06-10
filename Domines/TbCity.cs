
namespace Domines;

public partial class TbCity : BaseTable
{

    public string? CityAname { get; set; }

    public string? CityEname { get; set; }

    public Guid CountryId { get; set; }

    //public string? CountryAname { get; set; } = null!; 

    //public string? CountryEname { get; set; } = null!;

    public virtual TbCountry Country { get; set; } = null!;

    public virtual ICollection<TbUserReceiver> TbUserReceivers { get; set; } = new List<TbUserReceiver>();

    public virtual ICollection<TbUserSender> TbUserSebders { get; set; } = new List<TbUserSender>();
}
