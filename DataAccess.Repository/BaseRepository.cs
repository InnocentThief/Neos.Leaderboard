using DataAccess.Model;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace DataAccess.Repository
{
    public abstract class BaseRepository<TDatabaseContext> where TDatabaseContext : BaseContext
    {
        protected IConfiguration Configuration;

        public BaseRepository(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        /// <summary>
        /// Creates the corresponding database context.
        /// </summary>
        /// <returns>The corresponding database context.</returns>
        protected abstract TDatabaseContext GetDatabaseContext();
    }
}