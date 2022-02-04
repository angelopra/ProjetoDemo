using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Validators;
using FluentValidation;
using System;
using System.Security.Cryptography;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Model.Response;

namespace Business.CustomerBusiness
{
    public class CustomerComponent : BaseBusiness<ICustomerRepository>, ICustomerComponent
    {
        private readonly IValidator<CustomerRequest> _validator;
        private readonly IValidator<CustomerLoginRequest> _validatorLogin;
        private List<ValidateError> errors = null;
        private List<ValidateError> loginErrors = null;
        public CustomerComponent(ICustomerRepository context, IValidator<CustomerRequest> validator, IValidator<CustomerLoginRequest> validator_login)
            : base(context)
        {
            _validator = validator;
            _validatorLogin = validator_login;
        }

        public int AddCustomer(CustomerRequest request)
        {
            try
            {
                errors = ValidadeCustomerRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }
                
                var response = 0;

                var obj = request.Map<Customer>();
                
                if (_context.EmailExist(obj))
                {
                    throw new Exception("email is already in use");
                }

                Byte[] saltByte = GenerateSalt();

                obj.Hash = HashPassword(request.password, saltByte);
                obj.Salt = Convert.ToBase64String(saltByte);

                response = this._context.AddCustomer(obj);
                return response;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        public CustomerResponse GetCostumerById(int id)
        {
            try
            {
                var response = this._context.GetCustomerById(id);
                var customerResponse = response.Map<CustomerResponse>();
                return customerResponse;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public CustomerResponse Login(CustomerLoginRequest request)
        {
            try
            {
                // busco o id pelo email do customer para depois fazer um mapeamento do request para o tipo Customer
                // após isso busco o Salt do user com o id buscado e uso ele pra fazer o hashing da password que o usuário inseriu
                loginErrors = ValidadeCustomerLoginRequest(request);
                if (loginErrors != null)
                {
                    throw new Exception();
                }
                var requestMapped = request.Map<CustomerRequest>();
                Customer customerDB = _context.GetCustomerByCustomerRequest(requestMapped);

                var customerMapped = request.Map<Customer>();
                customerMapped.Id = customerDB.Id;
                customerMapped.Name = customerDB.Name;

                customerMapped.Salt = customerDB.Salt;

                byte[] salt = Convert.FromBase64String(customerMapped.Salt);

                customerMapped.Hash = HashPassword(request.password, salt);


                if (customerMapped.Hash == customerDB.Hash)
                {
                    var customerResponse = customerMapped.Map<CustomerResponse>();

                    Cart cart = new Cart();
                    cart.IdCustomer = customerResponse.Id;
                    cart.Active = true;
                    var idCart = _context.AddCart(cart);

                    customerResponse.IdCart = idCart;

                    return customerResponse;
                }
                throw new Exception("wrong password");
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
        public void Remove(int id)
        {
            try
            {
                this._context.Remove(id);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public CustomerResponse Update(CustomerRequest request, int id)
        {
            try
            {
                errors = ValidadeCustomerRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                Customer response;

                var obj = request.Map<Customer>();
                obj.Id = id;

                response = this._context.Update(obj);

                var customerResponse = response.Map<CustomerResponse>();
                return customerResponse;
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
        private List<ValidateError> ValidadeCustomerLoginRequest(CustomerLoginRequest request)
        {
            loginErrors = null;
            var validate = _validatorLogin.Validate(request);
            if (!validate.IsValid)
            {
                loginErrors = new List<ValidateError>();
                foreach (var failure in validate.Errors)
                {
                    var error = new ValidateError();
                    error.PropertyName = failure.PropertyName;
                    error.Error = failure.ErrorMessage;
                    loginErrors.Add(error);
                }
            }
            return loginErrors;
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

        private Byte[] GenerateSalt()
        {
            byte[] salt = new byte[128 / 8];
            using (var rngCsp = new RNGCryptoServiceProvider())
            {
                rngCsp.GetNonZeroBytes(salt);
            }
            //Convert.ToBase64String(salt)
            return salt;
        }
    }
}
