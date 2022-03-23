using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.CustomerRequests;
using Domain.Model.Response;
using Domain.Validators;
using FluentValidation;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CustomerBusiness.Update
{
    public class UpdateCustomer : ServiceManagerBase, IRequestHandler<UpdateCustomerRequest, CustomerResponse>
    {
        private List<ValidateError> errors;
        public UpdateCustomer(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<CustomerResponse> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidateObj<CustomerRequest>(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var obj = request.Map<Customer>();

                var email = _uow.Customer.Where(c => c.Email == request.Email).FirstOrDefault();
                if (email != null)
                {
                    throw new Exception("Email already in use");
                }

                _uow.Customer.Update(obj);
                await _uow.Commit(cancellationToken);

                return obj.Map<CustomerResponse>();
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
