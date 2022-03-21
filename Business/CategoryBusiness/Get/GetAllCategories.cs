using Business.Base;
using Domain.Common;
using Domain.Interfaces;
using Domain.Model.Request.CategoryRequests;
using Domain.Model.Response;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CategoryBusiness.Get
{
    public class GetAllCategories : ServiceManagerQueryBase, IRequestHandler<GetAllCategoriesRequest, PaginatedList<CategoryResponse>>
    {
        public GetAllCategories(IUnityOfWorkQuery uow) : base(uow)
        {
        }

        public async Task<PaginatedList<CategoryResponse>> Handle(GetAllCategoriesRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var categories = _uowQuery.Category;

                var response = categories.MappingEntityLinq<List<CategoryResponse>>().Paginate(request.PageNumber, request.PageSize);

                return response;
            }
            catch (Exception err)
            {
                MapperException(err);
                throw;
            }
        }
    }
}
