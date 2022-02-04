using Domain.Entities;
using Domain.Model.Request;
using Domain.Model.Response;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICartItemComponent
    {
        CartItemModelResponse AddCartItem(CartItemRequest request);
        CartItemModelResponse Update(CartItemUpdateRequest request, int idCart, int idProduct);
        void Remove(int idCart, int idProduct);
        CartItemModelResponse GetCartItem(int idCart, int idProduct);
        IEnumerable GetCartItens(int idCart, int? pageNumber, int? pageSize);
    }
}
