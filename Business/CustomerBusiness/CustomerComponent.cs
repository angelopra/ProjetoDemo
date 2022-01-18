using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CustomerBusiness
{
    public class CustomerComponent : BaseBusiness<ICustomerRepository>, ICustomerComponent
    {
        private readonly IValidator<CustomerRequest> _validator;
        public CustomerComponent(ICustomerRepository context, IValidator<CustomerRequest> validator) : base(context)
        {
            _validator = validator;
        }

        public int AddCustomer(CustomerRequest request)
        {
            try
            {
                if (!_validator.Validate(request).IsValid)
                {
                    throw new Exception("sus");
                }
                var response = 0;

                var obj = new Customer();
                obj.Name = request.Name;
                obj.Email = request.Email;
                obj.Active = request.Active;

                response = this._context.AddCustomer(obj);
                return response;
            }
            catch
            {
                throw;
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
                if (!_validator.Validate(request).IsValid)
                {
                    throw new Exception("sus");
                }
                Customer response;

                var obj = new Customer();
                obj.Id = id;
                obj.Name = request.Name;
                obj.Email = request.Email;
                obj.Active = request.Active;

                response = this._context.Update(obj);
                return response;
            }
            catch
            {
                throw;
            }
        }
    }
}
