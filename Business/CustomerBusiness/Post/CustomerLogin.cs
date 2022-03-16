using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Request.CustomerRequests;
using Domain.Model.Response;
using Domain.Validators;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CustomerBusiness.Post
{
    public class CustomerLogin : ServiceManagerBase, IRequestHandler<LoginCustomerRequest, CustomerResponse>
    {
        private readonly IValidator<CustomerLoginRequest> _validator;
        private List<ValidateError> errors;
        public CustomerLogin(IUnityOfWork uow, IValidator<CustomerLoginRequest> validator) : base(uow)
        {
            _validator = validator;
        }

        public async Task<CustomerResponse> Handle(LoginCustomerRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // busco o id pelo email do customer para depois fazer um mapeamento do request para o tipo Customer
                // após isso busco o Salt do user com o id buscado e uso ele pra fazer o hashing da password que o usuário inseriu
                errors = ValidadeCustomerLoginRequest(request.Map<CustomerLoginRequest>());
                if (errors != null)
                {
                    throw new Exception();
                }
                Customer customerDB = _uow.Customer.Where(c => c.Email == request.Email).FirstOrDefault();

                var customerMapped = request.Map<Customer>();
                customerMapped.Id = customerDB.Id;
                customerMapped.Name = customerDB.Name;

                customerMapped.Salt = customerDB.Salt;

                byte[] salt = Convert.FromBase64String(customerMapped.Salt);

                customerMapped.Hash = HashPassword(request.password, salt);

                if (customerMapped.Hash != customerDB.Hash)
                {
                    throw new Exception("Wrong password");
                }

                var customerResponse = customerMapped.Map<CustomerResponse>();

                var customerCart = _uow.Cart.Where(c => c.IdCustomer == customerResponse.Id).FirstOrDefault();

                if(customerCart != null)
                {
                    customerResponse.IdCart = customerCart.Id;
                }
                else
                {
                    customerResponse.IdCart = null;
                }

                return customerResponse;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        private List<ValidateError> ValidadeCustomerLoginRequest(CustomerLoginRequest request)
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
