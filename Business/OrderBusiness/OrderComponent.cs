﻿using Business.Base;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.OrderBusiness
{
    public class OrderComponent : BaseBusiness<IOrderRepository>, IOrderComponent
    {
    }
}