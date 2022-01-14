using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
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

        public IEnumerable<CartItem> GetCartItemsByCartId(int idCart)
        {
            try
            {
                var response = _context.GetCartItemsByCartId(idCart);
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

        public CartItemModelResponse Update(CartItemUpdateRequest request, int idCart, int idProduct)
        {
            try
            {
                CartItemModelResponse response;

                var cartItem = CartItemByIdProductAndByIdCart(idCart, idProduct);
                cartItem.Quantity = request.Quantity;
                cartItem.UnitPrice = request.UnitPrice;

                var responseDataBase = _context.Update(cartItem);

                response = new CartItemModelResponse();
                response.Id = responseDataBase.Id;
                response.Quantity = responseDataBase.Quantity;
                response.UnitPrice = responseDataBase.UnitPrice;
                response.IdCart = responseDataBase.IdCart;
                response.IdProduct = responseDataBase.IdProduct;

                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        private CartItem CartItemByIdProductAndByIdCart(int idCart, int idProduct)
        {
            var response = _context.CartItemByIdProductAndByIdCart(idCart, idProduct);
            return response;
        }
    }
}
