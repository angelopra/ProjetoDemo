using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
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
        public CustomerComponent(ICustomerRepository context) : base(context)
        {
        }

        public int AddCustomer(CustomerRequest request)
        {
            try
            {
                var response = 0;

                var obj = MappingEntity<Customer>(request);
                
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
                throw err;
            }
        }

        public CustomerResponse GetCostumerById(int id)
        {
            try
            {
                var response = this._context.GetCustomerById(id);
                var customerResponse = MappingEntity<CustomerResponse>(response);
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
                var requestMapped = MappingEntity<CustomerRequest>(request);
                Customer customerDB = _context.GetCustomerByCustomerRequest(requestMapped);

                var customerMapped = MappingEntity<Customer>(request);
                customerMapped.Id = customerDB.Id;

                customerMapped.Salt = customerDB.Salt;

                byte[] salt = Encoding.ASCII.GetBytes(customerMapped.Salt);

                customerMapped.Hash = HashPassword(request.password, salt);


                if (customerMapped.Hash == customerDB.Hash)
                {
                    var customerResponse = MappingEntity<CustomerResponse>(customerMapped);

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

                throw err;
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
                Customer response;

                var obj = MappingEntity<Customer>(request);
                obj.Id = id;

                response = this._context.Update(obj);

                var customerResponse = MappingEntity<CustomerResponse>(response);
                return customerResponse;
            }
            catch (Exception err)
            {
                throw err;
            }
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
