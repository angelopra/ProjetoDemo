using Domain.Model.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class CartItemUpdateValidator : AbstractValidator<CartItemUpdateRequest>
    {
        public CartItemUpdateValidator()
        {
            RuleFor(ciu => ciu.Active).NotNull();
            RuleFor(ciu => ciu.Quantity).NotNull().GreaterThan(0).WithMessage("Put at least one item in the cart.");
            RuleFor(ciu => ciu.UnitPrice).NotNull().GreaterThanOrEqualTo(0).WithMessage("Price cannot be negative.");
        }
    }
}
