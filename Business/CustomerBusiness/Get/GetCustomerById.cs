using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request.CustomerRequests;
using Domain.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CustomerBusiness.Get
{
    public class GetCustomerById : ServiceManagerBase, IRequestHandler<GetCustomerByIdRequest, Customer>
    {
        private List<ValidateError> errors;
        public GetCustomerById(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<Customer> Handle(GetCustomerByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.id < 0)
                {
                    throw new Exception();
                }

                var customer = _uow.Customer.Where(c => c.Id == request.id).FirstOrDefault();

                if (customer == null)
                {
                    throw new Exception("Customer doesn't exist");
                }

                return customer;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }

        }
    }
}
