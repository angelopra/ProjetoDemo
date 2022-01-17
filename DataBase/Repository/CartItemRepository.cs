using DataBase.Context;
using DataBase.Repository.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
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

        public CartItem AddCartItem(CartItem request)
        {
            try
            {
                _context.CartItem.Add(request);
                _context.SaveChanges();
                return request;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public IEnumerable GetCartItens(int idCart)
        {
            try
            {
                if(CartExists(idCart))
                {
                    List<CartItem> items = _context.CartItem.Where(c => c.IdCart == idCart).ToList();
                    return items;
                }
                else
                {
                    throw new Exception("Cart doesn't exists");
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

        private bool CartExists(int id)
        {
            try
            {
                var item = _context.Cart.Where(c => id == c.Id).FirstOrDefault();
                if(item == null)
                {
                    return false;
                }
                return true;
            }
            catch (Exception err)
            {
                throw err;

            }
        }

        public bool CartItemExists(CartItemRequest cartItem)
        {
            try
            {
                var item = _context.CartItem.Where(c => c.IdCart == cartItem.IdCart && c.IdProduct == cartItem.IdProduct).FirstOrDefault();
                if (item != null)
                    return true;

                return false;
            }
            catch(Exception err)
            {
                throw err;
            }

        }

        public CartItem IncreaseCartItem(CartItemRequest cartItem) // incrementa a quantidade de produtos e retorna o id do CartItem
        {   
            try
            {
                var item = CartItemByIdProductAndByIdCart(cartItem.IdCart, cartItem.IdProduct);
                item.Quantity += cartItem.Quantity;

                return item;
            }
            catch(Exception err)
            {
                throw err;
            }
        }
    }
}
