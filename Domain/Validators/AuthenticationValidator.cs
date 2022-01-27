using Domain.Model.Request;
using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Validators
{
    public class AuthenticationValidator : AbstractValidator<AuthenticationRequest>
    {
        public AuthenticationValidator()
        {
            RuleFor(c => c.UserName).NotNull().WithMessage("Informe o Username");
            RuleFor(c => c.Password).NotNull().NotEmpty().WithMessage("Informe o Password");
        }
    }
}
