﻿using Domain.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Model.Request.CustomerRequests
{
    public class LoginCustomerRequest : IRequest<CustomerResponse>
    {
        public string Email { get; set; }
        public string password { get; set; }
    }
}