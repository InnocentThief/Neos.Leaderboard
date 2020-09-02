using DataAccess.Entity.QuestEntity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model.Contexts
{
    /// <summary>
    /// Provides a session context with quest related database tables.
    /// </summary>
    public sealed class QuestContext : BaseContext
    {
        /// <summary>
        /// Represents the quest data table.
        /// </summary>
        public DbSet<Quest> Quest { get; set; }

        /// <summary>
        /// Represents the quest step data table.
        /// </summary>
        public DbSet<QuestStep> QuestStep { get; set; }
    }
}