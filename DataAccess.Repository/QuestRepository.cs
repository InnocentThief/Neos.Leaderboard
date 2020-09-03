using DataAccess.Entity.QuestEntity;
using DataAccess.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        /// <summary>
        /// Retrieves all quests associated to the given account key.
        /// </summary>
        /// <param name="accountKey">Unique identifier of the account for which to get all quests.</param>
        /// <returns>An awaitable task that returns a collection of <see cref="Quest"/>.</returns>
        public async Task<IEnumerable<Quest>> GetQuestsForAccountAsync(Guid accountKey)
        {
            using var context = GetDatabaseContext();
            return await context.Quest
                .AsNoTracking()
                .Where(q => q.AccountKey == accountKey)
                .OrderBy(q => q.Name)
                .ToListAsync();
        }


    }
}