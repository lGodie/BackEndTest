using Repositories.Shared.Core.Entities;
using Repositories.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace presupuestoback.Shared.v1.repositories
{
    public interface IUnitOfWork : IDisposable
    {   
        testContext Context { get; }
        void Commit();
    }
}
