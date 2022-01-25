using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class CustomerRepository : BaseRepository<CoreDbContext>, ICustomerRepository
    {
        public CustomerRepository(CoreDbContext context) : base(context)
        {
        }

        public int AddCustomer(Customer request)
        {
            try
            {
                _context.Customer.Add(request);
                _context.SaveChanges();
                return request.Id;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Customer GetCustomerById(int id)
        {
            try
            {
                Customer customer = this._context.Customer.Where(c => c.Id == id).FirstOrDefault();
                if (customer != null)
                {
                    return customer;
                }
                else
                {
                    throw new Exception("Customer doesn't exist");
                }
            }
            catch (Exception err)
            {

                throw err;
            }
        }
        public Customer GetCustomerByCustomerRequest(CustomerRequest request)
        {
            try
            {
                var customer = _context.Customer.Where(c => c.Email == request.Email).FirstOrDefault();
                if (customer != null)
                {
                    return customer;
                }
                else
                {
                    throw new Exception("Customer doesn't exist");
                }
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
                var customer = this._context.Customer.Where(c => c.Id == id).FirstOrDefault();
                if (customer != null)
                {
                    this._context.Customer.Remove(customer);
                    this._context.SaveChanges();
                }
                else
                {
                    throw new Exception("Customer doesn't exist");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Customer Update(Customer request)
        {
            try
            {
                this._context.Customer.Update(request);
                this._context.SaveChanges();
                return request;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int AddCart(Cart request)
        {
            try
            {
                _context.Cart.Add(request);
                _context.SaveChanges();
                return request.Id;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public bool EmailExist(Customer obj)
        {
            try
            {
                var customerDB = _context.Customer.Where(c => c.Email.Equals(obj.Email)).FirstOrDefault();
                if (customerDB != null)
                {
                    return true;
                }
                return false;
            }
            catch (Exception err)
            {

                throw err;
            }
        }
    }

}
