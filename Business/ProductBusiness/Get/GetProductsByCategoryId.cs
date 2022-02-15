using Business.Base;
using Domain.Interfaces;
using Domain.Model.Request.ProductRequests;
using Domain.Model.Response;
using Domain.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.ProductBusiness.Get
{
    public class GetProductsByCategoryId : ServiceManagerBase, IRequestHandler<GetProductsByCategoryIdRequest, List<ProductListResponse>>
    {
        private List<ValidateError> errors;
        public GetProductsByCategoryId(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<List<ProductListResponse>> Handle(GetProductsByCategoryIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = _uow.Category.Where(c => c.Id == request.categoryId).FirstOrDefault();
                if (category == null)
                {
                    throw new Exception("Category doesn't exist");
                }
                var products = _uow.Product.Where(p => p.Category.Id == request.categoryId);

                var response = products.Paginate(request.pageNumber, request.pageSize).Map<List<ProductListResponse>>();

                return response;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
