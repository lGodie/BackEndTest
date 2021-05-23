using Microsoft.Extensions.Configuration;
using presupuestoback.Shared.v1.repositories;
using Repositories.Shared.Core.Entities;
using Repositories.Shared.Repositories;
using System;
using System.Collections.Generic;
using System.Text;

namespace BackEndTest.Shared.Repositories.Generic
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly IConfiguration _configuration;
        private readonly string _connectionString;

        public testContext Context { get; }

        public UnitOfWork(IConfiguration configuration, testContext context)
        {
            _configuration = configuration;
            Context = context;
        }

        public void Commit()
        {
            Context.SaveChanges();
        }

        public void Dispose()
        {
            Context.Dispose();
        }

    }
}
