using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Entities;
using Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace DataBase.Repository
{
    public class CartRepository : BaseRepository<CoreDbContext>, ICartRepository
    {
        public CartRepository(CoreDbContext context) : base(context)
        {
        }

        public int AddCart(Cart request)
        {
            try
            {
                _context.Cart.Add(request);
                _context.SaveChanges();
                return request.Id;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public Cart GetCartById(int id)
        {
            try
            {
                Cart cart = _context.Cart.Where(c => c.Id == id).FirstOrDefault();
                if(cart != null)
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

        public void Remove(int id)
        {
            try
            {
                var cart = _context.Cart.Where(c => c.Id == id).FirstOrDefault();
                if(cart != null)
                {
                    _context.Cart.Remove(cart);
                    _context.SaveChanges();
                }
                else
                {
                    throw new Exception("Cart not found");
                }
            }
            catch(Exception err)
            {
                throw err;
            }
        }

        public int RemoveAllItems(int id)
        {
            try
            {
                // if the cart with the given id exists, remove all of its products
                if (_context.Cart.Where(c => c.Id == id).FirstOrDefault() != null)
                {
                    var numberDeleted = 0;  

                    List<CartItem> cartItems = _context.CartItem.Where(c => c.IdCart == id).ToList();
                    foreach(CartItem cartItem in cartItems)
                    {
                        _context.CartItem.Remove(cartItem);
                        numberDeleted++;
                    }
                    _context.SaveChanges();
                
                    return numberDeleted;
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

        public Cart Update(Cart request)
        {
            try
            {
                this._context.Cart.Update(request);
                this._context.SaveChanges();
                return request;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
