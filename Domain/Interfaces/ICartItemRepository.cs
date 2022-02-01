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
    public interface ICartItemRepository
    {
        CartItem AddCartItem(CartItem request);
        IQueryable<CartItem> GetCartItens(int idCart);
        CartItem Update(CartItem request);
        void Remove(int idCart, int idProduct);
        CartItem CartItemByIdProductAndByIdCart(int idCart, int idProduct);
        bool CartItemExists(CartItemRequest cartItem);
        CartItem IncreaseCartItem(CartItemRequest cartItem);
        Cart GetCartById(int idCart);
        void UpdateCart(Cart cart);
    }
}
