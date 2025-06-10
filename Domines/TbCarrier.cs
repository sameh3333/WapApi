
namespace Domines;

public partial class TbCarrier : BaseTable
{

    public string CarrierName { get; set; } = null!;

   

    public virtual ICollection<TbShippmentStatus> TbShippmentStatuses { get; set; } = new List<TbShippmentStatus>();
}
