using DataAccess.Repository;
using DataTransfer.Dto.Converter;
using DataTransfer.Dto.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardService.Business.Domains
{
    /// <summary>
    /// Provides quest related data handling.
    /// </summary>
    public sealed class QuestDomain
    {
        private readonly QuestRepository questRepository;

        public QuestDomain(IConfiguration configuration)
        {
            questRepository = new QuestRepository(configuration);
        }

        /// <summary>
        /// Retrieves all quests associated to the given account key.
        /// </summary>
        /// <param name="accountKey">Unique identifier of the account for which to get all quests.</param>
        /// <returns>An awaitable task that returns a collection of <see cref="QuestDto"/>.</returns>
        public async Task<IEnumerable<QuestDto>> GetQuestForAccountAsync(Guid accountKey)
        {
            var quests = await questRepository.GetQuestsForAccountAsync(accountKey);
            return quests.ToList().ToDtos();
        }

        /// <summary>
        /// Saves the given quest.
        /// </summary>
        /// <param name="questDto">Transfer object representing the quest to save.</param>
        public void SaveQuest(QuestDto questDto)
        {
            var entity = questDto.ToEntity();
            questRepository.Save(entity, ctx => ctx.Quest, q => q.QuestKey == questDto.QuestKey);
        }
    }
}