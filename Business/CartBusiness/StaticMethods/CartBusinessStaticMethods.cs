using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CartBusiness.StaticMethods
{
    public static class CartBusinessStaticMethods
    {
        public static Cart GetCartById(int id, IUnityOfWork _uow)
        {
            try
            {
                var cart = _uow.Cart.Where(c => c.Id == id).FirstOrDefault();
                if (cart != null)
                {
                    return cart;
                }
                else
                {
                    throw new Exception("Cart not found");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        public static bool CartItemExists(int idCart, int idProduct, IUnityOfWork _uow)
        {
            try
            {
                var item = _uow.CartItem.Where(c => c.IdCart == idCart && c.IdProduct == idProduct).Include(n => n.Cart).FirstOrDefault();
                if (item != null)
                    return true;
                return false;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public static CartItem GetCartItem(int idCart, int idProduct, IUnityOfWork _uow)
        {
            try
            {
                var response = _uow.CartItem.Where(c => c.IdCart == idCart && c.IdProduct == idProduct).Include(n => n.Cart).FirstOrDefault();
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
