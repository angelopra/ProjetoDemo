using Domain.Model.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class CustomerValidator : AbstractValidator<CustomerRequest>
    {
        public CustomerValidator()
        {
            RuleFor(cus => cus.Active).NotNull();
            RuleFor(cus => cus.Name).NotNull().Length(3, 50);
            RuleFor(cus => cus.Email).NotNull().EmailAddress();
        }
    }
}
