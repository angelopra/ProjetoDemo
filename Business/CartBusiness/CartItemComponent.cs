using Business.Base;
using Business.Pagination;
using DataBase.Repository;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Domain.Validators;
using FluentValidation;
using Microsoft.EntityFrameworkCore;
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
        private readonly IValidator<CartItemRequest> _validator;
        private readonly IValidator<CartItemUpdateRequest> _updateValidator;
        private List<ValidateError> errors;
        private List<ValidateError> updateErrors;

        public CartItemComponent(ICartItemRepository context, IValidator<CartItemRequest> validator, IValidator<CartItemUpdateRequest> updateValidator)
            : base(context)
        {
            _validator = validator;
            _updateValidator = updateValidator;
        }

        public CartItemModelResponse AddCartItem(CartItemRequest request)
        {
            try
            {
                errors = ValidadeCartItemRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                CartItem obj;

                // Updating correspondent cart Total value
                var cart = _context.GetCartById(request.IdCart);
                cart.Total += request.UnitPrice * request.Quantity;
                _context.UpdateCart(cart);

                if (CartItemExists(request))
                {
                    obj = IncreaseCartItem(request);
                    obj = _context.Update(obj);
                }
                else
                {
                    obj = request.Map<CartItem>();
                    obj = _context.AddCartItem(obj);
                }
                return obj.Map<CartItemModelResponse>();
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        public CartItemModelResponse GetCartItem(int idCart, int idProduct)
        {
            try
            {
                var item = CartItemByIdProductAndByIdCart(idCart, idProduct);
                var response = item.Map<CartItemModelResponse>();
                return response;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public IEnumerable GetCartItens(int idCart, int? pageNumber)
        {
            try
            {
                var paginatedItemList = _context.GetCartItens(idCart).Paginate<CartItem>(pageNumber, 2).Map<List<CartItemModelResponse>>();

                return paginatedItemList;
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
                var cartItem = CartItemByIdProductAndByIdCart(idCart, idProduct);

                // Updating correspondent cart Total value
                var cart = _context.GetCartById(idCart);
                cart.Total -= cartItem.UnitPrice * cartItem.Quantity;
                _context.UpdateCart(cart);

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
                updateErrors = ValidadeCartItemUpdateRequest(request);
                if (updateErrors != null)
                {
                    throw new Exception();
                }
                var cartItem = CartItemByIdProductAndByIdCart(idCart, idProduct);

                // Updating correspondent cart Total value
                var cart = _context.GetCartById(idCart);
                if (cart.IsClosed)
                {
                    throw new Exception("Cart is closed, cannot update");
                }
                decimal initialTotal = cartItem.Quantity * cartItem.UnitPrice;
                decimal finalTotal = request.Quantity * request.UnitPrice;
                decimal difference = finalTotal - initialTotal;
                cart.Total += difference;
                _context.UpdateCart(cart);

                cartItem.Quantity = request.Quantity;
                cartItem.UnitPrice = request.UnitPrice;

                var responseDataBase = _context.Update(cartItem);

                CartItemModelResponse response = responseDataBase.Map<CartItemModelResponse>();

                return response;
            }
            catch (Exception err)
            {
                MapperException(err, updateErrors);
                throw;
            }
        }
        private CartItem CartItemByIdProductAndByIdCart(int idCart, int idProduct)
        {
            var query = _context.GetCartItem();
            var response = query.Where(c => c.IdCart == idCart && c.IdProduct == idProduct).Include(n => n.Cart).FirstOrDefault();
            if (response == null)
            {
                throw new Exception("cart or product doesn't exist");
            }
            return response;
        }

        private bool CartItemExists(CartItemRequest cartItem)
        {
            var query = _context.GetCartItem();

            var item = query.Where(c => c.IdCart == cartItem.IdCart && c.IdProduct == cartItem.IdProduct).FirstOrDefault();
            if (item != null)
                return true;

            return false;
        }

        private CartItem IncreaseCartItem(CartItemRequest cartItem)
        {
            try
            {
                var item = CartItemByIdProductAndByIdCart(cartItem.IdCart, cartItem.IdProduct);
                item.Quantity += cartItem.Quantity;
                return item;
            }
            catch (Exception err)
            {

                throw err;
            }
        }

        private List<ValidateError> ValidadeCartItemRequest(CartItemRequest request)
        {
            errors = null;
            var validate = _validator.Validate(request);
            if (!validate.IsValid)
            {
                errors = new List<ValidateError>();
                foreach (var failure in validate.Errors)
                {
                    var error = new ValidateError();
                    error.PropertyName = failure.PropertyName;
                    error.Error = failure.ErrorMessage;
                    errors.Add(error);
                }
            }
            return errors;
        }
        private List<ValidateError> ValidadeCartItemUpdateRequest(CartItemUpdateRequest request)
        {
            updateErrors = null;
            var validate = _updateValidator.Validate(request);
            if (!validate.IsValid)
            {
                updateErrors = new List<ValidateError>();
                foreach (var failure in validate.Errors)
                {
                    var error = new ValidateError();
                    error.PropertyName = failure.PropertyName;
                    error.Error = failure.ErrorMessage;
                    updateErrors.Add(error);
                }
            }
            return updateErrors;
        }
    }
}
