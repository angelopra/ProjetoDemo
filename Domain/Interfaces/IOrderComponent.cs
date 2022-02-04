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
    public interface IOrderComponent
    {
        public OrderResponse CreateOrder(OrderRequest request);
        public OrderResponse GetOrderById(int id);
        public List<OrderResponse> GetCustomerOrders(int customerId);
        public void RemoveOrder(int orderId);
    }
}
