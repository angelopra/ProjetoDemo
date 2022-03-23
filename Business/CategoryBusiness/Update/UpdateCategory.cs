using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Messengers.QueueType.CategoryQueues;
using Domain.Model.Request;
using Domain.Model.Request.CategoryRequests;
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

namespace Business.CategoryBusiness.Update
{
    public class UpdateCategory : ServiceManagerBase, IRequestHandler<UpdateCategoryRequest, CategoryResponse>
    {
        private List<ValidateError> errors;
        private readonly IMessengerBusClient _messenger;
        private readonly CategoryUpdateQueue _categoryUpdateQueue;


        public UpdateCategory(IUnityOfWork uow
            ,IMessengerBusClient messenger
            ,CategoryUpdateQueue categoryUpdateQueue) : base(uow)
        {
            _messenger = messenger;
            _categoryUpdateQueue = categoryUpdateQueue;
        }

        public async Task<CategoryResponse> Handle(UpdateCategoryRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidateObj<CategoryRequest>(request);
                if (errors != null)
                {
                    throw new Exception();
                }
                if(_uow.Category.Where(c => c.Name == request.Name).FirstOrDefault() != null)
                {
                    throw new Exception("Category name already in use");
                }

                var obj = request.Map<Category>();

                _uow.Category.Update(obj);
                await _uow.Commit(cancellationToken);

                _messenger.Publish(_categoryUpdateQueue.QueueName, obj, _categoryUpdateQueue.RoutingKey, _categoryUpdateQueue.Exchange);

                return obj.Map<CategoryResponse>();
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
