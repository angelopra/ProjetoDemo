using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class UnitOfWork : BaseRepository<CoreDbContext>, IUnityOfWork
    {
        private ICartItemRepository _cartItem;

        private ICartRepository _cart;

        private ICategoryRepository _category;

        private IProductRepository _product;

        private ICustomerRepository _customer;

        private IOrderRepository _order;

        protected UnitOfWork(CoreDbContext context) : base(context)
        {
        }

        public ICartItemRepository cartItemRepository

        {
            get 
            { 
                 if(_cartItem == null)
                 {
                    _cartItem = new CartItemRepository(_context);
                 }
                 return _cartItem;
            }
        }

        public ICartRepository cartRepository
        {
            get
            {
                if (_cart == null)
                {
                    _cart = new CartRepository(_context);
                }
                return _cart;
            }
        }

        public ICategoryRepository categoryRepository
        {
            get
            {
                if(_category == null)
                {
                    _category = new CategoryRepository(_context);
                }
                return _category;
            }
        }

        public IProductRepository productRepository
        {
            get
            {
                if(_product == null)
                {
                    _product = new ProductRepository(_context);
                }
                return _product;
            }
        }

        public ICustomerRepository customerRepository
        {
            get
            {
                if(_customer == null)
                {
                    _customer = new CustomerRepository(_context);
                }
                return _customer;
            }
        }

        public IOrderRepository orderRepository
        {
            get
            {
                if(_order == null)
                {
                    _order = new OrderRepository(_context);
                }
                return _order;
            }
        }
       
        public void Commit()
        {
            _context.SaveChanges();
        }
    }
}
