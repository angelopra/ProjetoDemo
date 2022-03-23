using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.CartRequests;
using Domain.Validators;
using FluentValidation;
using MediatR;

namespace Business.CartBusiness.Post
{
    public class PostCart : ServiceManagerBase, IRequestHandler<PostCartRequest, int>
    {
        private List<ValidateError> errors;
        public PostCart(IUnityOfWork uow) : base(uow)
        {
        }

        public async Task<int> Handle(PostCartRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidateObj<CartRequest>(request);
                if (errors != null)
                {
                    throw new Exception();
                }
                var obj = request.Map<Cart>();

                _uow.Cart.Add(obj);
                await _uow.Commit(cancellationToken);

                return obj.Id;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
