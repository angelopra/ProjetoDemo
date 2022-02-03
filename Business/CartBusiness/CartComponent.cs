using System;
using Business.Base;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Entities.Base;
using Domain.Entities;
using FluentValidation;
using Domain.Validators;
using Domain.Model.Response;

namespace Business.CartBusiness
{
    public class CartComponent : BaseBusiness<ICartRepository>, ICartComponent
    {
        private readonly IValidator<CartRequest> _validator;
        private List<ValidateError> errors;
        public CartComponent(ICartRepository context, IValidator<CartRequest> validator) : base(context)
        {
            _validator = validator;
        }

        public int AddCart(CartRequest request)
        {
            try
            {
                errors = ValidadeCartRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var response = 0;

                var obj = request.Map<Cart>();

                response = this._context.AddCart(obj);
                return response;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        public CartResponse GetCartById(int id)
        {
            try
            {
                var response = this._context.GetCartById(id);
                var mappedResponse = response.Map<CartResponse>();
                return mappedResponse;
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
                this._context.Remove(id);
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public int RemoveAllItems(int id)
        {
            try
            {
                var numberDeleted = _context.RemoveAllItems(id);
                var cart = _context.GetCartById(id);
                cart.Total = 0;
                _context.Update(cart);

                return numberDeleted;
            }
            catch (Exception err)
            {
                throw err;
            }
        }

        public CartResponse Update(CartRequest request, int id)
        {
            try
            {
                errors = ValidadeCartRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var cart = _context.GetCartById(id);
                cart.Active = request.Active;
                cart.IdCustomer = request.IdCustomer;
                cart.IsClosed = request.IsClosed;
                var response = _context.Update(cart);
                var responseMapped = response.Map<CartResponse>();
                return responseMapped;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        private List<ValidateError> ValidadeCartRequest(CartRequest request)
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
    }
}
