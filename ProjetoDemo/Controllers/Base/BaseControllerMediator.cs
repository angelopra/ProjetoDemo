using Domain.Interfaces;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace ProjetoDemo.Controllers.Base
{
    public abstract class BaseControllerMediator : ControllerBase
    {
        public BaseControllerMediator(IHelper helper)
        {
            _helper = helper;
        }
        protected IHelper _helper;

        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();
    }
}
