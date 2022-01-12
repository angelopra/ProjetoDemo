using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CustomerBusiness
{
    public class CustomerComponent : BaseBusiness<ICustomerRepository>, ICustomerComponent
    {
        public CustomerComponent(ICustomerRepository context) : base(context)
        {
        }

        public int AddCustomer(CustomerRequest request)
        {
            try
            {
                var response = 0;

                var obj = new Customer();
                obj.Name = request.Name;
                obj.Email = request.Email;
                obj.Active = request.Active;

                response = this._context.AddCustomer(obj);
                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Customer GetCostumerById(int id)
        {
            try
            {
                var response = this._context.GetCustomerById(id);
                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Remove(int id)
        {
            try
            {
                this._context.Remove(id);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Customer Update(CustomerRequest request, int id)
        {
            try
            {
                Customer response;

                var obj = new Customer();
                obj.Id = id;
                obj.Name = request.Name;
                obj.Email = request.Email;
                obj.Active = request.Active;

                response = this._context.Update(obj);
                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
