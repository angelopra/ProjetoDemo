using Domain.Entities;
using Domain.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICartItemComponent
    {
        int AddCartItem(CartItemRequest request);
        CartItem Update(CartItemUpdateRequest request, int idCart, int idProduct);
        void Remove(int idCart, int idProduct);
        CartItem GetCartItemById(int idCart, int idProduct);
    }
}
