using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Moq;
using Xunit;

namespace ProjetoDemoTests
{
    public class UnitTest1
    {
        [Fact]
        public void Test1()
        {
            var orderRepository = new Mock<IOrderRepository>();
            var orderRequest = new OrderRequest()
            {
                Discounts = 10,
                IdCart = 10
            };
            var order = new Order()
            {
                Discounts = orderRequest.Discounts,
                IdCart = orderRequest.IdCart
            };
            orderRepository.Setup(or => or.CreateOrder(It.IsAny<Order>())).Returns(order);
        }
    }
}
