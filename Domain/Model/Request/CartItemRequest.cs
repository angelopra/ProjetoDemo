﻿using Domain.Model.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request
{
    public class CartItemRequest : BaseRequest
    {
        public int IdCart { get; set; }
        public int IdProduct { get; set; }
        public decimal UnitPrice { get; set; }
        public int Quantity { get; set; }
    }
}
