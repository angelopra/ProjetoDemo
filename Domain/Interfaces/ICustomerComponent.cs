using Domain.Entities;
using Domain.Model.Request;
using Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerComponent
    {
        int AddCustomer(CustomerRequest request);
        CustomerResponse Login(CustomerLoginRequest request);
        CustomerResponse GetCostumerById(int id);
        CustomerResponse Update(CustomerRequest request, int id);
        void Remove(int id);
    }
}
