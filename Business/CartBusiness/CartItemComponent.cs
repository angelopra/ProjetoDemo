using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.CartBusiness
{
    public class CartItemComponent : BaseBusiness<ICartItemRepository>, ICartItemComponent
    {
        public CartItemComponent(ICartItemRepository context) : base(context)
        {
        }

        public int AddCartItem(CartItemRequest request)
        {
            try
            {
                var response = 0;

                var obj = new CartItem();
                obj.Active = request.Active;
                obj.IdCart = request.IdCart;
                obj.IdProduct = request.IdProduct;
                obj.UnitPrice = request.UnitPrice;
                obj.Quantity = request.Quantity;

                response = _context.AddCartItem(obj);
                return response;
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
                var response = _context.GetCartItemById(idCart, idProduct);
                return response;
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
                _context.Remove(idCart, idProduct);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public CartItem Update(CartItemUpdateRequest request, int idCart, int idProduct)
        {
            try
            {
                CartItem response;

                var obj = new CartItem();
                obj.Active = request.Active;
                obj.IdCart = idCart;
                obj.IdProduct = idProduct;
                obj.UnitPrice = request.UnitPrice;
                obj.Quantity = request.Quantity;

                response = _context.Update(obj);
                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
    }
}
