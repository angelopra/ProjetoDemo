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
        private readonly IValidator<CustomerRequest> _validator;
        private List<ValidateError> errors;

        public PostCustomer(IUnityOfWork uow, IValidator<CustomerRequest> validator)
            : base(uow)
        {
            _validator = validator;
        }

        public async Task<CustomerResponse> Handle(PostCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                errors = ValidadeCustomerRequest(request.Map<CustomerRequest>());
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

        private List<ValidateError> ValidadeCustomerRequest(CustomerRequest request)
        {
            errors = null;
            var validate = _validator.Validate(request);
            if (!validate.IsValid)
            {
                errors = new List<ValidateError>();
                foreach (var failure in validate.Errors)
                {
                    var error = new ValidateError();
                    error.PropertyName = failure.PropertyName;
                    error.Error = failure.ErrorMessage;
                    errors.Add(error);
                }
            }
            return errors;
        }

        private Byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            return salt;
        }

        private String HashPassword(String password, Byte[] salt)
        {
            string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
            password: password,
            salt: salt,
            prf: KeyDerivationPrf.HMACSHA256,
            iterationCount: 100000,
            numBytesRequested: 256 / 8));

            return hashed;
        }
    }
}
