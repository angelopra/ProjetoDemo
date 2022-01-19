using Domain.Model.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class CartValidator : AbstractValidator<CartRequest>
    {
        public CartValidator()
        {
            RuleFor(c => c.Active).NotNull();
            RuleFor(c => c.IdCustomer).NotNull().GreaterThanOrEqualTo(0).WithMessage("Enter a valid ID.");
            RuleFor(c => c.IsClosed).NotNull();
        }
    }
}
