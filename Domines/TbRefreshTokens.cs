﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domines
{
    public  class TbRefreshTokens : BaseTable
    {
        public string Token { get; set; }               // التوكن الفعلي
        public string UserId { get; set; }               
        public DateTime ExpiryDate { get; set; }

    }
}
