using Domain.Entities;
using Domain.Model.Request;
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
        Customer GetCostumerById(int id);
        Customer Update(CustomerRequest request, int id);
        void Remove(int id);
    }
}
