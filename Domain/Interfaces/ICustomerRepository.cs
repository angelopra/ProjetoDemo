using Domain.Entities;
using Domain.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICustomerRepository
    {
        int AddCustomer(Customer request);

        Customer GetCustomerById(int id);
        Customer GetCustomerByCustomerRequest(CustomerRequest request);
        Customer Update(Customer request);
        int AddCart(Cart request);
        bool EmailExist(Customer obj);
        void Remove(int id);
    }
}
