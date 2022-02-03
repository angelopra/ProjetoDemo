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
    public class OrderRepository : BaseRepository<CoreDbContext>, IOrderRepository
    {
        public OrderRepository(CoreDbContext context) : base(context)
        {
        }

        public Order CreateOrder(Order order)
        {
            try
            {
                _context.Order.Add(order);
                _context.SaveChanges();
                return order;
            }
            catch
            {
                throw;
            }
        }

        public Cart GetCartById(int idCart)
        {
            try
            {
                Cart cart = _context.Cart.Where(c => c.Id == idCart).FirstOrDefault();
                if(cart == null)
                {
                    throw new Exception("Cart not found");
                }
                return cart;
            }
            catch
            {
                throw;
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                Order order = _context.Order.Where(o => o.Id == id).FirstOrDefault();
                if (order == null)
                {
                    throw new Exception("Order not found");
                }
                return order;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public IQueryable<Order> GetCustomerOrders(int customerId)
        {
            try
            {
                var carts = _context.Cart.Where(c => c.IdCustomer == customerId).ToList();
                var orders = new List<Order>();
                foreach (var cart in carts)
                {
                    orders.Add(_context.Order.Where(o => o.IdCart == cart.Id).FirstOrDefault());
                }

                return (IQueryable<Order>)orders;
            }
            catch
            {
                throw;
            }
        }

        public List<CartItem> GetItemsByCartId(int idCart)
        {
            try
            {
                var cartItems = _context.CartItem.Where(ci => ci.IdCart == idCart).ToList();
                return cartItems;
            }
            catch
            {
                throw;
            }
        }

        public void RemoveOrder(int orderId)
        {
            try
            {
                var order = _context.Order.Where(o => o.Id == orderId).FirstOrDefault();
                if (order == null)
                {
                    throw new Exception("Order not found");
                }
                _context.Order.Remove(order);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }

        public void UpdateCart(Cart cart)
        {
            try
            {
                _context.Cart.Update(cart);
                _context.SaveChanges();
            }
            catch
            {
                throw;
            }
        }
    }
}
