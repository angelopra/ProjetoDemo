using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Validators;
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
        private List<ValidateError> errors = null;
        public CustomerComponent(ICustomerRepository context, IValidator<CustomerRequest> validator) : base(context)
        {
            _validator = validator;
        }

        public int AddCustomer(CustomerRequest request)
        {
            try
            {
                errors = ValidadeCustomerRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }
                
                var response = 0;

                var obj = MappingEntity<Customer>(request);

                response = this._context.AddCustomer(obj);
                return response;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
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
                errors = ValidadeCustomerRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                Customer response;

                var obj = MappingEntity<Customer>(request);
                obj.Id = id;

                response = _context.Update(obj);
                return response;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        private List<ValidateError> ValidadeCustomerRequest(CustomerRequest request)
        {
            errors = null;
            var validate = _validator.Validate(request);
            if (!validate.IsValid)
            {
                errors = new List<ValidateError>();
                foreach (var failure in validate.Errors)
                {
                    var error = new ValidateError();
                    error.PropertyName = failure.PropertyName;
                    error.Error = failure.ErrorMessage;
                    errors.Add(error);
                }
            }
            return errors;
        }
    }
}
