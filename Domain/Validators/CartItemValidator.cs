using Domain.Model.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class CartItemValidator : AbstractValidator<CartItemRequest>
    {
        public CartItemValidator()
        {
            RuleFor(ci => ci.Active).NotNull();
            RuleFor(ci => ci.IdCart).NotNull();
            RuleFor(ci => ci.IdProduct).NotNull();
            RuleFor(ci => ci.Quantity).NotNull().GreaterThan(0).WithMessage("Put at least one item in the cart.");
            RuleFor(ci => ci.UnitPrice).NotNull().GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
        }
    }
}
