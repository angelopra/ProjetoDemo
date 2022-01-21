using Business.Base;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.OrderBusiness
{
    public class OrderComponent : BaseBusiness<IOrderRepository>, IOrderComponent
    {
        public OrderComponent(IOrderRepository context) : base(context)
        {
        }

        public CloseOrderResponse CloseOrder(int id)
        {
            throw new NotImplementedException();
        }

        public OrderResponse CreateOrder(OrderRequest request)
        {
            throw new NotImplementedException();
        }

        public OrderResponse GetOrderById(int id)
        {
            throw new NotImplementedException();
        }

        public int RemoveOrder(int orderId)
        {
            throw new NotImplementedException();
        }

        public OrderResponse UpdateOrder(OrderUpdateRequest request)
        {
            throw new NotImplementedException();
        }
    }
}
