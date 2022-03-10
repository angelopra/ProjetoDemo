﻿using Business.Base;
using Domain.Entities;
using Domain.Interfaces;
using Domain.Model.Request.CartRequests;
using Domain.Validators;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Business.CartBusiness.Remove
{
    public class RemoveAllItems : ServiceManagerBase, IRequestHandler<RemoveAllItemsRequest, int>
    {
        private List<ValidateError> errors;
        private ICartBusinessMethods _cbm;
        public RemoveAllItems(IUnityOfWork uow, ICartBusinessMethods cbm) : base(uow)
        {
            _cbm = cbm;
        }

        public async Task<int> Handle(RemoveAllItemsRequest request, CancellationToken cancellationToken)
        {
            try
            {
                // if the cart with the given id exists, remove all of its products
                if (_uow.Cart.Where(c => c.Id == request.Id).FirstOrDefault() != null)
                {
                    var numberDeleted = 0;

                    List<CartItem> cartItems = _uow.CartItem.Where(c => c.IdCart == request.Id).ToList();
                    foreach (CartItem cartItem in cartItems)
                    {
                        _uow.CartItem.Remove(cartItem);
                        numberDeleted++;
                    }

                    var cart = _cbm.GetCart(request.Id);
                    cart.Total = 0;

                    _uow.Cart.Update(cart);
                    await _uow.Commit(cancellationToken);

                    return numberDeleted;
                }
                else
                {
                    throw new Exception("Cart not found");
                }
            }
            catch (Exception err)
            {
                MapperException(err, errors);
                throw;
            }
        }
        
    }
}