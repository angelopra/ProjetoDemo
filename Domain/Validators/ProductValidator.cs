using Domain.Model.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class ProductValidator : AbstractValidator<ProductRequest>
    {
        public ProductValidator()
        {
            RuleFor(p => p.Active).NotNull();
            RuleFor(p => p.Description).MaximumLength(255);
            RuleFor(p => p.IdCategory).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p.Name).NotNull().Length(3, 30);
            RuleFor(p => p.Price).NotNull().GreaterThanOrEqualTo(0);
            RuleFor(p => p.Quantity).NotNull().GreaterThanOrEqualTo(0).WithMessage("Enter a positive value.");
        }
    }
}
