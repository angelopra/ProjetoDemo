using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnityOfWorkBase
    {
        ICartItemRepository cartItemRepository { get; }
        ICartRepository cartRepository { get; }
        ICategoryRepository categoryRepository { get; }
        IProductRepository productRepository { get; }
        ICustomerRepository customerRepository { get; }
        IOrderRepository orderRepository { get; }
    }
}
