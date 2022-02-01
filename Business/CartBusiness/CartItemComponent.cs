using Business.Base;
using Business.Pagination;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Domain.Validators;
using FluentValidation;
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
                    obj = MappingEntity<CartItem>(request);
                    obj = _context.AddCartItem(obj);
                }
                return MappingEntity<CartItemModelResponse>(obj); 
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
                var response = MappingEntity<CartItemModelResponse>(item);
                return response;
            }
            catch(Exception err)
            {
                throw err;
            }
        }

        public IEnumerable GetCartItens(int idCart, int? pageNumber)
        {
            try
            {
                List<CartItemModelResponse> response = new List<CartItemModelResponse>();

                var paginatedItemList = PaginatedList<CartItem>.Create(_context.GetCartItens(idCart), pageNumber ?? 1, 10);
                foreach (CartItem item in paginatedItemList)
                {
                    response.Add(MappingEntity<CartItemModelResponse>(item));
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

                CartItemModelResponse response = MappingEntity<CartItemModelResponse>(responseDataBase); 

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
            var response = _context.CartItemByIdProductAndByIdCart(idCart, idProduct);
            return response;
        }

        private bool CartItemExists(CartItemRequest cartItem)
        {
            return _context.CartItemExists(cartItem);
        }

        private CartItem IncreaseCartItem(CartItemRequest cartItem)
        {
            return _context.IncreaseCartItem(cartItem);
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
