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

        /// <summary>
        /// Initializes a new <see cref="QuestDomain"/>.
        /// </summary>
        /// <param name="configuration">Provides access to configuration data.</param>
        public QuestDomain(IConfiguration configuration)
        {
            questRepository = new QuestRepository(configuration);
        }

        /// <summary>
        /// Deletes the quest with the given key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest.</param>
        public void DeleteQuest(Guid questKey)
        {
            questRepository.DeleteQuest(questKey);
        }

        /// <summary>
        /// Deletes the quest step with the given key.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the quest step.</param>
        public void DeleteQuestStep(Guid questStepKey)
        {
            questRepository.DeleteQuestStep(questStepKey);
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

        public async Task<IEnumerable<QuestStepDto>> GetQuestStepsAsync(Guid questKey)
        {
            var questSteps = await questRepository.GetQuestStepsAsync(questKey);
            return questSteps.ToList().ToDtos();
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

        /// <summary>
        /// Saves the given quest step.
        /// </summary>
        /// <param name="questStepDto">Transfer object representing the quest step to save.</param>
        public void SaveQuestStep(QuestStepDto questStepDto)
        {
            var entity = questStepDto.ToEntity();
            questRepository.Save(entity, ctx => ctx.QuestStep, qs => qs.QuestStepKey == questStepDto.QuestStepKey);
        }
    }
}