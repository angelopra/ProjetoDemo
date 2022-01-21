using Domain.Model.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class CustomerLoginValidator : AbstractValidator<CustomerLoginRequest>
    {
        public CustomerLoginValidator()
        {
            RuleFor(cus => cus.Active).NotNull();
            RuleFor(cus => cus.password).NotNull().Length(7, 30);
            RuleFor(cus => cus.Email).NotNull().EmailAddress();
        }
    }
}
