using Business.Base;
using Domain.Interfaces;
using Domain.Model.Request.CategoryRequests;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CategoryBusiness.Remove
{
    public class RemoveCategory : ServiceManagerBase, IRequestHandler<RemoveCategoryRequest, int>
    {
        public RemoveCategory(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<int> Handle(RemoveCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                var category = _uow.Category.Where(c => c.Id == request.Id).FirstOrDefault();
                if (category == null)
                {
                    throw new Exception("Category doesn't exist");
                }

                _uow.Category.Remove(category);
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
