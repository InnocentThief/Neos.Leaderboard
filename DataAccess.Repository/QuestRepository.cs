using DataAccess.Entity.QuestEntity;
using DataAccess.Model.Contexts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
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
        /// Deletes the quest with the given key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest to delete.</param>
        public void DeleteQuest(Guid questKey)
        {
            using var context = GetDatabaseContext();
            Delete(context => context.Quest, q => q.QuestKey == questKey);
        }

        /// <summary>
        /// Deletes the quest step with the given key.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the quest step to delete.</param>
        public void DeleteQuestStep(Guid questStepKey)
        {
            using var context = GetDatabaseContext();
            Delete(context => context.QuestStep, qs => qs.QuestStepKey == questStepKey);
        }

        /// <summary>
        /// Retrieves the quest for the given key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest.</param>
        /// <returns>An awaitable task that returns the requested <see cref="Quest"/>.</returns>
        public async Task<Quest> GetQuestAsync(Guid questKey)
        {
            using var context = GetDatabaseContext();
            return await context.Quest
                .AsNoTracking()
                .Include(q => q.QuestSteps)
                .SingleOrDefaultAsync(q => q.QuestKey == questKey);
        }

        /// <summary>
        /// Retrieves all quests associated to the given account key.
        /// </summary>
        /// <param name="accountKey">Unique identifier of the account for which to get all quests.</param>
        /// <returns>An awaitable task that returns a collection of <see cref="Quest"/>.</returns>
        /// <remarks>Includes the quest steps.</remarks>
        public async Task<IEnumerable<Quest>> GetQuestsForAccountAsync(Guid accountKey)
        {
            using var context = GetDatabaseContext();
            return await context.Quest
                .AsNoTracking()
                .Include(q => q.QuestSteps)
                .Where(q => q.AccountKey == accountKey)
                .OrderBy(q => q.Name)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves the quest step for the given quest step key.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the quest step.</param>
        /// <returns>An awaitable task that returns the requested quest step.</returns>
        public async Task<QuestStep> GetQuestStepAsync(Guid questStepKey)
        {
            using var context = GetDatabaseContext();
            return await context.QuestStep
                .AsNoTracking()
                .SingleOrDefaultAsync(qs => qs.QuestStepKey == questStepKey);
        }

        /// <summary>
        /// Retrieves the max sort order for existing quest steps for the given quest key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest.</param>
        /// <returns>An awaitable task that returns the max sort order.</returns>
        public async Task<int> GetNextSortOrderAsync(Guid questKey)
        {
            using var context = GetDatabaseContext();
            return await context.QuestStep
                .AsNoTracking()
                .Where(qs => qs.QuestKey == questKey)
                .MaxAsync(qs => qs.SortOrder);
        }

        /// <summary>
        /// Retrieves the quest step for the given quest with the next sort order .
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest.</param>
        /// <param name="sortOrder">The sort order of the quest step to check for next quest steps.</param>
        /// <returns>An awaitable task that returns the requested quest step.</returns>
        public async Task<QuestStep> GetNextQuestStepAsync(Guid questKey, int sortOrder)
        {
            using var context = GetDatabaseContext();
            return await context.QuestStep
                .AsNoTracking()
                .Where(qs => qs.QuestKey == questKey)
                .Where(qs => qs.SortOrder == sortOrder + 1)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves the quest step for the given quest with the previous sort order .
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest.</param>
        /// <param name="sortOrder">The sort order of the quest step to check for previous quest steps.</param>
        /// <returns>An awaitable task that returns the requested quest step.</returns>
        public async Task<QuestStep> GetPreviousQuestStepAsync(Guid questKey, int sortOrder)
        {
            using var context = GetDatabaseContext();
            return await context.QuestStep
                .AsNoTracking()
                .Where(qs => qs.QuestKey == questKey)
                .Where(qs => qs.SortOrder == sortOrder - 1)
                .SingleOrDefaultAsync();
        }

        /// <summary>
        /// Retrieves the quest steps for the given quest key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the associated quest.</param>
        /// <returns>An awaitable task that returns a collection of quest steps.</returns>
        public async Task<IEnumerable<QuestStep>> GetQuestStepsAsync(Guid questKey)
        {
            using var context = GetDatabaseContext();
            return await context.QuestStep
                .AsNoTracking()
                .Include(qs => qs.QuestStepProgressions)
                .Where(qs => qs.QuestKey == questKey)
                .OrderBy(qs => qs.SortOrder)
                .ToListAsync();
        }

        /// <summary>
        /// Retrieves the leader board entries for a given quest.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest for which to get the leader board.</param>
        /// <returns>An awaitable task that returns a collection of <see cref="QuestLeaderboardEntry"/>.</returns>
        public async Task<IEnumerable<QuestLeaderboardEntry>> GetLeaderboardAsync(Guid questKey)
        {
            using var context = GetDatabaseContext();
            return await context.QuestStepProgression
                .AsNoTracking()
                .Include(qsp => qsp.Account)
                .Where(qsp => qsp.QuestStep.QuestKey == questKey)
                .GroupBy(qsp => qsp.Account.Username)
                .Select(g => new QuestLeaderboardEntry { Username = g.Key, QuestStepsDone = g.Count() })
                .OrderByDescending(lb => lb.QuestStepsDone)
                .ToListAsync();
        }
    }
}