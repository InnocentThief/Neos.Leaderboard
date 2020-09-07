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

        public async Task<bool> IsPreviousQuestStepDone(Guid questKey, Guid accountKey, int previousQuestStepOrder)
        {
            using var context = GetDatabaseContext();
            return await context.QuestStepProgression
                .AsNoTracking()
                .Where(qsp => qsp.QuestStep.QuestKey == questKey)
                .Where(qsp => qsp.AccountKey == accountKey)
                .Where(qsp => qsp.QuestStep.SortOrder == previousQuestStepOrder)
                .AnyAsync();
        }
    }
}
