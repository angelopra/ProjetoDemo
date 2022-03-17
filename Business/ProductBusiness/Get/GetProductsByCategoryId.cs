using Business.Base;
using Domain.Common;
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
    public class GetProductsByCategoryId : ServiceManagerQueryBase, IRequestHandler<GetProductsByCategoryIdRequest, PaginatedList<ProductListResponse>>
    {
        private List<ValidateError> errors;
        public GetProductsByCategoryId(IUnityOfWorkQuery uow) : base(uow)
        {
        }

        public async Task<PaginatedList<ProductListResponse>> Handle(GetProductsByCategoryIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = _uowQuery.Category.Where(c => c.Id == request.categoryId).FirstOrDefault();
                if (category == null)
                {
                    throw new Exception("Category doesn't exist");
                }
                var products = _uowQuery.Product.Where(p => p.IdCategory == request.categoryId);

                var response = products.MappingEntityLinq<List<ProductListResponse>>().Paginate(request.pageNumber, request.pageSize);

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
