using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request.ProductRequests;
using Domain.Validators;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.ProductBusiness.Get
{
    public class GetProductById : ServiceManagerBase, IRequestHandler<GetProductByIdRequest, Product>
    {
        private List<ValidateError> errors;
        public GetProductById(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<Product> Handle(GetProductByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.id < 0)
                {
                    throw new Exception();
                }

                var product = _uow.Product.Where(c => c.Id == request.id).Include(p => p.Category).FirstOrDefault();

                if (product == null)
                {
                    throw new Exception("Product doesn't exist");
                }

                return product;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }

        }
    }
}
