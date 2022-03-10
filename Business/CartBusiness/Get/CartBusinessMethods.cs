using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Business.CartBusiness.Get
{
    public class CartBusinessMethods : ServiceManagerBase, ICartBusinessMethods
    {
        public CartBusinessMethods(IUnityOfWork uow) : base(uow)
        {
        }

        public Cart GetCart(int id)
        {
            try
            {
                var cart = _uow.Cart.Where(c => c.Id == id).FirstOrDefault();
                if (cart == null)
                {
                    throw new Exception("Cart not found");
                }
                return cart;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public bool CartItemExists(int idCart, int idProduct)
        {
            try
            {
                var item = _uow.CartItem
                            .Where(c => c.IdCart == idCart && c.IdProduct == idProduct)
                            .Include(n => n.Cart).FirstOrDefault();
                if (item != null)
                    return true;
                return false;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public CartItem GetCartItem(int idCart, int idProduct)
        {
            try
            {
                var response = _uow
                                .CartItem.Where(c => c.IdCart == idCart && c.IdProduct == idProduct)
                                .Include(n => n.Cart).FirstOrDefault();
                if (response == null)
                {
                    throw new Exception("cart or product doesn't exist");
                }
                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
