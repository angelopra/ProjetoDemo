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
        private readonly IValidator<CustomerRequest> _validator;

        public UpdateCustomer(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<CustomerResponse> Handle(UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            errors = ValidadeCustomerRequest(request.Map<CustomerRequest>());
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
