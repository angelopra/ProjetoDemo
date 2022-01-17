using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using System;
using System.Collections;
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

        public int AddCartItem(CartItemRequest request) // talvez mudar isso aqui pra retornar o CartItemModelResponse ou o CartItem (retornar o objeto inteiro pra facilitar a vida do front end pq ele n precisaria fazer um get)
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

        public CartItemModelResponse GetCartItem(int idCart, int idProduct)
        {
            try
            {
                var item = CartItemByIdProductAndByIdCart(idCart, idProduct);
                var response = CartItemMapper(item);
                return response;
            }
            catch(Exception err)
            {
                throw err;
            }
        }

        public IEnumerable GetCartItems(int idCart)
        {
            try
            {
                var responseDataBase = _context.GetCartItems(idCart);
                List<CartItemModelResponse> response = new List<CartItemModelResponse>();

                foreach(CartItem item in responseDataBase)
                {
                    response.Add(CartItemMapper(item));
                }
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
        
        public CartItemModelResponse CartItemMapper(CartItem request)
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

        private bool CartItemExists(CartItemRequest cartItem)
        {
            return _context.CartItemExists(cartItem);
        }

        private int IncreaseCartItem(CartItemRequest cartItem)
        {
            return _context.IncreaseCartItem(cartItem);
        }
    }
}
