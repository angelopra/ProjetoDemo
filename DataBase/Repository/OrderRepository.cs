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
            catch (Exception err)
            {
                throw err;
            }
        }

        public Cart GetCartById(int idCart)
        {
            try
            {
                Cart cart = _context.Cart.Where(c => c.Id == idCart).FirstOrDefault();
                if(cart != null)
                {
                    return cart;
                }
                else
                {
                    throw new Exception("Cart not found");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Order GetOrderById(int id)
        {
            try
            {
                Order order = _context.Order.Where(o => o.Id == id).FirstOrDefault();
                if (order != null)
                {
                    return order;
                }
                else
                {
                    throw new Exception("Order not found");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<Order> GetOrdersByCustomerId(int customerId)
        {
            try
            {
                var carts = _context.Cart.Where(c => c.IdCustomer == customerId).ToList();
                List<Order> orders = new List<Order>();
                foreach (var cart in carts)
                {
                    orders.Add(_context.Order.Where(o => o.IdCart == cart.Id).FirstOrDefault());
                }

                return orders;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public List<CartItem> GetItemsByCartId(int idCart)
        {
            try
            {
                var cartItems = _context.CartItem.Where(ci => ci.IdCart == idCart).ToList();
                return cartItems;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void RemoveOrder(int orderId)
        {
            try
            {
                var order = _context.Order.Where(o => o.Id == orderId).FirstOrDefault();
                if (order != null)
                {
                    _context.Order.Remove(order);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cart Item not found");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void UpdateCart(Cart cart)
        {
            try
            {
                _context.Cart.Update(cart);
                _context.SaveChanges();
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
