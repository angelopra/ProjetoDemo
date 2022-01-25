using Domain.Entities;
using Domain.Model.Request;
using Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IOrderRepository
    {
        public Order CreateOrder(Order order);
        public Order GetOrderById(int id);
        public List<CartItem> GetItemsByCartId(int idCart);
        public List<Order> GetOrdersByCustomerId(int customerId);
        public void RemoveOrder(int orderId);
        public Cart GetCartById(int idCart);
        public void UpdateCart(Cart cart);
    }
}
