using Business.Base;
using Domain.Interfaces;
using Domain.Model.Request.CustomerRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CustomerBusiness.Remove
{
    public class RemoveCustomer : ServiceManagerBase, IRequestHandler<RemoveCustomerRequest, int>
    {
        public RemoveCustomer(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<int> Handle(RemoveCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var customer = _uow.Customer.Where(c => c.Id == request.Id).FirstOrDefault();
                if (customer == null)
                {
                    throw new Exception("Customer doesn't exist");
                }

                _uow.Customer.Remove(customer);
                await _uow.Commit(cancellationToken);

                return request.Id;
            }
            catch (Exception err)
            {
                MapperException(err);
                throw;
            }
        }
    }
}
