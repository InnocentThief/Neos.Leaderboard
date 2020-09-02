using DataAccess.Entity.QuestEntity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model
{
    /// <summary>
    /// Abstract database context.
    /// </summary>
    public abstract class BaseContext : DbContext
    {
        /// <summary>
        /// Configures the <see cref="BaseContext"/> to connect to the proper sql server database.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <exception cref="ConfigurationErrorsException">Thrown if the connection string is not configured.</exception>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // TODO: Get Connectionstring from Service-Config
        }

        /// <summary>
        /// Defines composite primary keys, inheritance, and relationships using Fluent API.
        /// </summary>
        /// <param name="modelBuilder">Allows to configure entities, relationships between them, and how they map to the database.</param>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            CreateRelationshipDefinitions(modelBuilder);
        }

        /// <summary>
        /// Creates relationship definitions b declaring navigation properties and foreign keys.
        /// </summary>
        private void CreateRelationshipDefinitions(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Quest>().HasOne(q => q.Account).WithMany(a => a.Quests).HasForeignKey(q => q.AccountKey);
            modelBuilder.Entity<QuestStep>().HasOne(qs => qs.Quest).WithMany(q => q.QuestSteps).HasForeignKey(qs => qs.QuestKey);
        }
    }
}