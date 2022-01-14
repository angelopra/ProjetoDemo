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
    public interface ICartItemComponent
    {
        int AddCartItem(CartItemRequest request);
        CartItemModelResponse Update(CartItemUpdateRequest request, int idCart, int idProduct);
        void Remove(int idCart, int idProduct);
        IEnumerable<CartItem> GetCartItemsByCartId(int idCart);
    }
}
