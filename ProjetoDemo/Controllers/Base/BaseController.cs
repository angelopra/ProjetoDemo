using MediatR;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDemo.Controllers.Base
{
    public abstract class BaseController<T> : ControllerBase
    {
        protected BaseController([FromServices] T contract, IMediator mediator)
        {
            this.ComponentCurrent = contract;
            _mediator = mediator;
        }
        public IMediator _mediator { get; }
        public T ComponentCurrent { get; }
    }
}
