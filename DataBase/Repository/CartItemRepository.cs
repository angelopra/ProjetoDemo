using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBase.Repository
{
    public class CartItemRepository : BaseRepository<CoreDbContext>, ICartItemRepository
    {
        public CartItemRepository(CoreDbContext context) : base(context)
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

        public List<CartItem> GetCartItemsByCartId(int idCart)
        {
            try
            {
                var cartItemsEntities = _context.CartItem.Where(c => c.IdCart == idCart).ToList();

                if (cartItemsEntities != null)
                {
                    return cartItemsEntities;
                }
                else
                {
                    throw new Exception("Card was not found or there is no item in the cart");
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

        public CartItem CartItemByIdProductAndByIdCart(int idCart, int idProduct)
        {
            var response = _context.CartItem.Where(c => c.IdCart == idCart && c.IdProduct == idProduct)
                    .Include(n => n.Cart)
                    .FirstOrDefault();
            return response;
        }
    }
}
