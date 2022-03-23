using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.CustomerRequests;
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
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace Business.CustomerBusiness.Post
{
    public class PostCustomer : ServiceManagerBase, IRequestHandler<PostCustomerRequest, CustomerResponse>
    {
        private List<ValidateError> errors;

        public PostCustomer(IUnityOfWork uow)
            : base(uow)
        {
        }

        public async Task<CustomerResponse> Handle(PostCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidateObj<CustomerRequest>(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var obj = request.Map<Customer>();

                Byte[] saltByte = GenerateSalt();

                obj.Hash = HashPassword(request.password, saltByte);
                obj.Salt = Convert.ToBase64String(saltByte);

                await _uow.Customer.AddAsync(obj);
                await _uow.Commit(cancellationToken);

                return obj.Map<CustomerResponse>();
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
    }
}
