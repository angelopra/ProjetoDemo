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

        public int AddCartItem(CartItemRequest request) // talvez mudar isso aqui pra retornar o CartItemModelResponse (retornar o objeto inteiro)
        {
            try
            {
                if(CartItemExists(request))
                {
                    var id = IncreaseCartItem(request);
                    return id;
                }
                else
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

        public CartItemModelResponse Update(CartItemUpdateRequest request, int idCart, int idProduct)
        {
            try
            {

                var cartItem = CartItemByIdProductAndByIdCart(idCart, idProduct);
                cartItem.Quantity = request.Quantity;
                cartItem.UnitPrice = request.UnitPrice;

                var responseDataBase = _context.Update(cartItem);

                CartItemModelResponse response = CartItemMapper(responseDataBase);

                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }
        private CartItemModelResponse CartItemMapper(CartItem request)
        {
            CartItemModelResponse response;

            response = new CartItemModelResponse();
            response.Id = request.Id;
            response.Quantity = request.Quantity;
            response.UnitPrice = request.UnitPrice;
            response.IdCart = request.IdCart;
            response.IdProduct = request.IdProduct;

            return response;
        }

        private CartItem CartItemByIdProductAndByIdCart(int idCart, int idProduct)
        {
            var response = _context.CartItemByIdProductAndByIdCart(idCart, idProduct);
            return response;
        }

        public bool CartItemExists(CartItemRequest cartItem)
        {
            return _context.CartItemExists(cartItem);
        }

        public int IncreaseCartItem(CartItemRequest cartItem)
        {
            return _context.IncreaseCartItem(cartItem);
        }
    }
}
