﻿using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjetoDemo.Controllers.Base
{
    public abstract class BaseController<T> : ControllerBase
    {
        protected BaseController([FromServices] T contract)
        {
            this.ComponentCurrent = contract;
        }
        public T ComponentCurrent { get; }
    }
}
