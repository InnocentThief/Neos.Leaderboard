using DataAccess.Entity.QuestEntity;
using DataAccess.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    /// <summary>
    /// Provides quest step progression based data access.
    /// </summary>
    public class QuestStepProgressionRepository : BaseRepository<QuestContext>
    {
        /// <summary>
        /// Initializes a new <see cref="QuestStepProgressionRepository"/>.
        /// </summary>
        /// <param name="configuration">Provides access to configuration data.</param>
        public QuestStepProgressionRepository(IConfiguration configuration) : base(configuration)
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
        /// Gets the quest step progression for the given quest step and account.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the quest step.</param>
        /// <param name="username">Name of the user that solved the quest step.</param>
        /// <returns>An awaitable task that returns the requested <see cref="QuestStepProgression"/>.</returns>
        public async Task<QuestStepProgression> GetQuestStepProgressionAsync(Guid questStepKey, string username)
        {
            using var context = GetDatabaseContext();
            return await context.QuestStepProgression
                .SingleOrDefaultAsync(qsp => qsp.QuestStepKey == questStepKey && qsp.Username == username);
        }

        /// <summary>
        /// Retrieves if the previous quest step has been done by the account.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest.</param>
        /// <param name="username">Nae of the user that solved the quest step.</param>
        /// <param name="previousQuestStepOrder">The quest step order to check.</param>
        /// <returns>An awaitable task that returns true if the previous quest step has been done.</returns>
        public async Task<bool> IsPreviousQuestStepDone(Guid questKey, string username, int previousQuestStepOrder)
        {
            using var context = GetDatabaseContext();
            return await context.QuestStepProgression
                .AsNoTracking()
                .Where(qsp => qsp.QuestStep.QuestKey == questKey)
                .Where(qsp => qsp.Username == username)
                .Where(qsp => qsp.QuestStep.SortOrder == previousQuestStepOrder)
                .AnyAsync();
        }
    }
}
