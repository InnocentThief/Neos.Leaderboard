using DataAccess.Model;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repository
{
    /// <summary>
    /// Abstract base class for repositories.
    /// </summary>
    /// <typeparam name="TDatabaseContext"></typeparam>
    public abstract class BaseRepository<TDatabaseContext> where TDatabaseContext : BaseContext
    {
        protected IConfiguration Configuration;

        /// <summary>
        /// Initializes a new <see cref="BaseRepository{TDatabaseContext}"/>.
        /// </summary>
        /// <param name="configuration">Provides access to configuration data.</param>
        protected BaseRepository(IConfiguration configuration)
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