using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

<<<<<<< HEAD:Business/CartBusiness/CartBusinessMethods.cs
namespace Business.CartBusiness
=======
namespace Business.CartBusiness.Get
>>>>>>> 42ba7dbe2345c031a1408df16e1a5b7b92e4c928:Business/CartBusiness/Get/CartBusinessMethods.cs
{
    public class CartBusinessMethods : ServiceManagerBase, ICartBusinessMethods
    {
        public CartBusinessMethods(IUnityOfWork uow) : base(uow)
        {
        }

<<<<<<< HEAD:Business/CartBusiness/CartBusinessMethods.cs
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

=======
>>>>>>> 42ba7dbe2345c031a1408df16e1a5b7b92e4c928:Business/CartBusiness/Get/CartBusinessMethods.cs
        public Cart GetCartById(int id)
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
<<<<<<< HEAD:Business/CartBusiness/CartBusinessMethods.cs
=======

>>>>>>> 42ba7dbe2345c031a1408df16e1a5b7b92e4c928:Business/CartBusiness/Get/CartBusinessMethods.cs
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
