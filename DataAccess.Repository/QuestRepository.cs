using DataAccess.Model.Contexts;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Repository
{
    /// <summary>
    /// Provides quest based data access.
    /// </summary>
    public sealed class QuestRepository : BaseRepository<QuestContext>
    {
        /// <summary>
        /// Initializes a new <see cref="QuestRepository"/>.
        /// </summary>
        /// <param name="configuration">Provides access to configuration data.</param>
        public QuestRepository(IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Creates the corresponding database context.
        /// </summary>
        /// <returns>The corresponding database context.</returns>
        protected override QuestContext GetDatabaseContext()
        {
            return new QuestContext(Configuration);
        }
    }
}