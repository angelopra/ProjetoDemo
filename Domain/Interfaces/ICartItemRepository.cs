﻿using Domain.Entities;
using Domain.Model.Request;
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

        CartItem GetCartItemById(int idCart, int idProduct);

        CartItem Update(CartItem request);

        void Remove(int idCart, int idProduct);
        CartItem CartItemByIdProductAndByIdCart(int idCart, int idProduct);
        bool CartItemExists(CartItemRequest cartItem);
        int IncreaseCartItem(CartItemRequest cartItem);
    }
}
