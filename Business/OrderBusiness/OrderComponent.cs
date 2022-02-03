using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request;
using Domain.Model.Response;
using Domain.Validators;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.OrderBusiness
{
    public class OrderComponent : BaseBusiness<IOrderRepository>, IOrderComponent
    {
        private readonly IValidator<OrderRequest> _validator;
        private List<ValidateError> errors = null;
        public OrderComponent(IOrderRepository context, IValidator<OrderRequest> validator) : base(context)
        {
            _validator = validator;
        }

        public OrderResponse CreateOrder(OrderRequest request)
        {
            try
            {
                errors = ValidadeOrderRequest(request);
                if (errors != null)
                {
                    throw new Exception();
                }

                var obj = MappingEntity<Order>(request);

                // Getting cart info
                var cartTotal = _context.GetCartById(request.IdCart).Total;
                obj.Subtotal = cartTotal;
                obj.Total = cartTotal - request.Discounts;

                var order = _context.CreateOrder(obj);

                var response = order.Map<OrderResponse>();
                var items = _context.GetItemsByCartId(request.IdCart);

                response.Items = items.Map<List<CartItemModelResponse>>();

                CloseCart(request.IdCart);

                return response;
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }

        public OrderResponse GetOrderById(int id)
        {
            try
            {
                var order = _context.GetOrderById(id);
                var items = _context.GetItemsByCartId(order.IdCart);
                var response = order.Map<OrderResponse>();

                response.Items = items.Map<List<CartItemModelResponse>>();

                return response;
            }
            catch
            {
                throw;
            }
        }

        public List<OrderResponse> GetCustomerOrders(int customerId)
        {
            try
            {
                var responseDataBase = _context.GetCustomerOrders(customerId);
                var response = new List<OrderResponse>();
                List<CartItem> items;
                OrderResponse orderResponse;

                foreach (Order order in responseDataBase)
                {
                    items = new List<CartItem>();
                    orderResponse = order.Map<OrderResponse>();
                    items = _context.GetItemsByCartId(order.IdCart);
                    orderResponse.Items = items.Map<List<CartItemModelResponse>>();
                    response.Add(orderResponse);
                }
                return response;
            }
            catch
            {
                throw;
            }
        }

        public void RemoveOrder(int orderId)
        {
            try
            {
                _context.RemoveOrder(orderId);
            }
            catch
            {
                throw;
            }
        }

        private List<ValidateError> ValidadeOrderRequest(OrderRequest request)
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

        private void CloseCart(int idCart)
        {
            var cart = _context.GetCartById(idCart);
            cart.IsClosed = true;
            _context.UpdateCart(cart);
        }
    }
}
