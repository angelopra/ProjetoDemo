﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Domain.Interfaces
{
    public interface IUnityOfWork : IUnityOfWorkBase
    {
        Task<int> Commit(CancellationToken cancellationToken);
    }
}
