using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request.ProductRequests;
using Domain.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Business.ProductBusiness.Remove
{
    public class RemoveProduct : ServiceManagerBase, IRequestHandler<RemoveProductRequest, int>
    {
        private List<ValidateError> errors;
        public RemoveProduct(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<int> Handle(RemoveProductRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var product = _uow.Product.Where(c => c.Id == request.id).FirstOrDefault();
                if (product == null)
                {
                    throw new Exception("Product doesn't exist");
                }

                _uow.Product.Remove(product);
                await _uow.Commit(cancellationToken);

                return request.id;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
