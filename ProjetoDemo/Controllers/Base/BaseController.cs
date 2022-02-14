using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDemo.Controllers.Base
{
    public abstract class BaseController<T> : ControllerBase
    {
        private ISender _mediator;

        protected ISender Mediator => _mediator ??= HttpContext.RequestServices.GetService<ISender>();

        protected BaseController([FromServices] T contract, IMediator _mediator123)
        {
            this.ComponentCurrent = contract;
        }
        public T ComponentCurrent { get; }
    }
}
