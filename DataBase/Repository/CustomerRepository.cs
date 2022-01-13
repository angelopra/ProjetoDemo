using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Entities;
using Domain.Interfaces;
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
            Customer customer = this._context.Customer.Where(c => c.Id == id).FirstOrDefault();
            return customer;
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
            }
            catch (Exception err)
            {
                throw;
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
                throw;
            }
        }
    }
}
