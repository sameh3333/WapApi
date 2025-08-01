﻿using System;
using System.Collections.Generic;

namespace Domines;

public partial class TbUserSender : BaseTable
{

    public Guid UserId { get; set; }

    public string SenderName { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string Phone { get; set; } = null!;
    public string PostalCode { get; set; }
    public string Contact { get; set; } = null!;
    public string OtherAddress { get; set; } = null!;
    public bool IsDefalte { get; set; } 

    public Guid CityId { get; set; }

    public string Address { get; set; } = null!;

    

    public virtual TbCity City { get; set; } = null!;

    public virtual ICollection<TbShippment> TbShippments { get; set; } = new List<TbShippment>();
}
