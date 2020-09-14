using DataAccess.Entity.QuestEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Model
{
    /// <summary>
    /// Abstract database context.
    /// </summary>
    public abstract class BaseContext : DbContext
    {
        private IConfiguration configuration;

        public BaseContext(IConfiguration configuration)
        {
            this.configuration = configuration;
        }

        /// <summary>
        /// Configures the <see cref="BaseContext"/> to connect to the proper sql server database.
        /// </summary>
        /// <param name="optionsBuilder"></param>
        /// <exception cref="ConfigurationErrorsException">Thrown if the connection string is not configured.</exception>
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(configuration.GetConnectionString("Default"));
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
            modelBuilder.Entity<QuestStepProgression>().HasOne(qsp => qsp.QuestStep).WithMany(qs=>qs.QuestStepProgressions).HasForeignKey(qsp => qsp.QuestStepKey);
            modelBuilder.Entity<QuestStepProgression>().HasOne(qsp => qsp.Account).WithMany().HasForeignKey(qsp => qsp.AccountKey);
        }
    }
}