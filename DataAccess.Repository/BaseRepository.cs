using DataAccess.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Linq.Expressions;

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

        /// <summary>
        /// Deletes the entity with the given primary key selector.
        /// </summary>
        /// <typeparam name="TEntity">The datatabe of the entity to be deleted.</typeparam>
        /// <param name="dbCollection">Represents the data table where the entity will be delete / removed from.</param>
        /// <param name="primaryKeySelector">A predicate to locate the unique identifier of the existing entity.</param>
        public void Delete<TEntity>(Func<TDatabaseContext, DbSet<TEntity>> dbCollection, Expression<Func<TEntity, bool>> primaryKeySelector) where TEntity : class
        {
            using var context = GetDatabaseContext();
            var original = dbCollection(context).SingleOrDefault(primaryKeySelector);
            if (original != null)
            {
                context.Entry(original).State = EntityState.Deleted;
                context.SaveChanges();
            }
        }

        /// <summary>
        /// Saves the given entity to the database.
        /// </summary>
        /// <typeparam name="TEntity">The datatype of the entity to be saved.</typeparam>
        /// <param name="entity">The entity to be saved.</param>
        /// <param name="dbCollection">Represents the data table where the entity will be stored.</param>
        /// <param name="predicate">A predicate to locate the unique identifier of the entity.</param>
        public void Save<TEntity>(TEntity entity, Func<TDatabaseContext, DbSet<TEntity>> dbCollection, Expression<Func<TEntity, bool>> predicate) where TEntity : class
        {
            using var context = GetDatabaseContext();
            var original = dbCollection(context).SingleOrDefault(predicate);
            if (original == null)
            {
                dbCollection(context).Attach(entity);
                context.Entry(entity).State = EntityState.Added;
            }
            else
            {
                context.Entry(original).CurrentValues.SetValues(entity);
            }
            context.SaveChanges();
        }
    }
}