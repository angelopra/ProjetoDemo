using Business.Base;
using Domain.Interfaces;
using Domain.Model.Request.CategoryRequests;
using Domain.Model.Response;
using Domain.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CategoryBusiness.Get
{
    public class GetCategoryById : ServiceManagerBase, IRequestHandler<GetCategoryByIdRequest, CategoryResponse>
    {
        private List<ValidateError> errors;

        public GetCategoryById(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<CategoryResponse> Handle(GetCategoryByIdRequest request, CancellationToken cancellationToken)
        {
            try
            {
                if (request.Id < 0)
                {
                    throw new Exception();
                }

                var category = _uow.Category.Where(c => c.Id == request.Id).FirstOrDefault();

                if (category == null)
                {
                    throw new Exception("Category doesn't exist");
                }

                return category.Map<CategoryResponse>();
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }

        }
    }
}
