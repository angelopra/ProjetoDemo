using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Entities;
using Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    internal class CartItemRepository : BaseRepository<CoreDbContext>, ICartItemRepository
    {
        protected CartItemRepository(CoreDbContext context) : base(context)
        {
        }

        public int AddCartItem(CartItem request)
        {
            try
            {
                _context.CartItem.Add(request);
                _context.SaveChanges();
                return request.Id;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public CartItem GetCartItemById(int idCart, int idProduct)
        {
            try
            {
                CartItem cartItem = _context.CartItem.Where(c => c.IdCart == idCart && c.IdProduct == idProduct).FirstOrDefault();
                if (cartItem != null)
                {
                    return cartItem;
                }
                else
                {
                    throw new Exception("Cart Item not found");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public void Remove(int idCart, int idProduct)
        {
            try
            {
                var cartItem = _context.CartItem.Where(c => c.IdCart == idCart && c.IdProduct == idProduct).FirstOrDefault();
                if (cartItem != null)
                {
                    _context.CartItem.Remove(cartItem);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cart Item not found");
                }
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public CartItem Update(CartItem request)
        {
            try
            {
                _context.CartItem.Update(request);
                _context.SaveChanges();
                return request;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
