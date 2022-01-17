using Domain.Entities;
using Domain.Model.Response;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface ICartItemRepository
    {
        int AddCartItem(CartItem request);
        List<CartItem> GetCartItemsByCartId(int idCart);
        CartItem Update(CartItem request);
        void Remove(int idCart, int idProduct);
        CartItem CartItemByIdProductAndByIdCart(int idCart, int idProduct);
        Cart GetCartById(int idCart);
        void UpdateCart(Cart cart);
    }
}
