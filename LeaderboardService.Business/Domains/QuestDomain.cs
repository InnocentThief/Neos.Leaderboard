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
        #region Private fields

        private readonly QuestRepository questRepository;
        private readonly QuestStepProgressionRepository questStepProgressionRepository;

        #endregion

        /// <summary>
        /// Initializes a new <see cref="QuestDomain"/>.
        /// </summary>
        /// <param name="configuration">Provides access to configuration data.</param>
        public QuestDomain(IConfiguration configuration)
        {
            questRepository = new QuestRepository(configuration);
            questStepProgressionRepository = new QuestStepProgressionRepository(configuration);
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
        /// Retriees the quest for the given key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest.</param>
        /// <returns>An awaitable task that returns a <see cref="QuestDto"/>.</returns>
        public async Task<QuestDto> GetQuestAsync(Guid questKey)
        {
            var quest = await questRepository.GetQuestAsync(questKey);
            return quest.ToDto();
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
        /// Retrieves the requested quest step.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the quest step.</param>
        /// <returns>An awaitable task that returns the requested <see cref="QuestStepDto"/>.</returns>
        public async Task<QuestStepDto> GetQuestStepAsync(Guid questStepKey)
        {
            // Get quest step
            var questStep = await questRepository.GetQuestStepAsync(questStepKey);

            // Get quest progression
            var hasQuestProgression = await questRepository.HasQuestProgresssionAsync(questStep.QuestKey);

            // Add progression to dto
            var retval = questStep.ToDto();
            retval.CanReorder = !hasQuestProgression;
            return retval;
        }

        /// <summary>
        /// Retrieves the quest step for the given quest.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest.</param>
        /// <returns>An awaitable task that returns a collection of <see cref="QuestStepDto"/>.</returns>
        public async Task<IEnumerable<QuestStepDto>> GetQuestStepsAsync(Guid questKey)
        {
            // Get quest steps
            var questSteps = await questRepository.GetQuestStepsAsync(questKey);

            // Get quest progression
            var hasQuestProgression = await questRepository.HasQuestProgresssionAsync(questKey);

            // Add progression to quest steps
            var retval = questSteps.ToList().ToDtos();
            foreach (var questStep in retval)
            {
                questStep.CanReorder = !hasQuestProgression;
            }
            return retval;
        }

        /// <summary>
        /// Retrieves the leader board for the given guest key.
        /// </summary>
        /// <param name="questKey"></param>
        /// <returns>An awaitable task that returns the requested leader board.</returns>
        public async Task<IEnumerable<QuestLeaderboardEntryDto>> GetLeaderboardAsync(Guid questKey)
        {
            var leaderboardEntries = await questRepository.GetLeaderboardAsync(questKey);
            return leaderboardEntries.ToList().ToDtos();
        }

        /// <summary>
        /// Retrieves the next quest step fro the given quest and user.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest.</param>
        /// <param name="username">Username for which to get the next quest step.</param>
        /// <returns>An awaitable task that returns a quest step.</returns>
        public async Task<QuestStepDto> GetNextQuestStepForUserAsync(Guid questKey, string username)
        {
            var questSteps = await questRepository.GetQuestStepsAsync(questKey);
            if (!questSteps.Any()) return null;
            var lastQuestStepDone = await questStepProgressionRepository.GetLastStepDoneAsync(questKey, username);
            if (lastQuestStepDone == null)
            {
                return questSteps.First().ToDto();
            }
            else
            {
                var nextQuestStep = questSteps.SingleOrDefault(qs => qs.SortOrder == lastQuestStepDone.QuestStep.SortOrder + 1);
                if (nextQuestStep != null) return nextQuestStep.ToDto();
                return null;
            }
        }

        /// <summary>
        /// Moves the quest step with the given key on place up (and changes the previous quest step.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the quest step.</param>
        /// <returns>An awaitable task that yields no retrun value.</returns>
        public async Task MoveDownAsync(Guid questStepKey)
        {
            var questStep = await questRepository.GetQuestStepAsync(questStepKey);

            var previousQuestStep = await questRepository.GetPreviousQuestStepAsync(questStep.QuestKey, questStep.SortOrder);
            if (previousQuestStep == null) return;
            previousQuestStep.SortOrder++;
            questRepository.Save(previousQuestStep, ctx => ctx.QuestStep, qs => qs.QuestStepKey == previousQuestStep.QuestStepKey);
            questStep.SortOrder--;
            questRepository.Save(questStep, ctx => ctx.QuestStep, qs => qs.QuestStepKey == questStep.QuestStepKey);
        }

        /// <summary>
        /// Moves the quest step with the given key on place down (and changes the next quest step.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the quest step.</param>
        /// <returns>An awaitable task that yields no return value.</returns>
        public async Task MoveUpAsync(Guid questStepKey)
        {
            var questStep = await questRepository.GetQuestStepAsync(questStepKey);

            var nextQuestStep = await questRepository.GetNextQuestStepAsync(questStep.QuestKey, questStep.SortOrder);
            if (nextQuestStep == null) return;
            nextQuestStep.SortOrder--;
            questRepository.Save(nextQuestStep, ctx => ctx.QuestStep, qs => qs.QuestStepKey == nextQuestStep.QuestStepKey);
            questStep.SortOrder++;
            questRepository.Save(questStep, ctx => ctx.QuestStep, qs => qs.QuestStepKey == questStep.QuestStepKey);
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
        public async Task SaveQuestStep(QuestStepDto questStepDto)
        {
            var entity = questStepDto.ToEntity();

            var hasQuestSteps = await questRepository.HasQuestStepsAsync(questStepDto.QuestKey);
            if (hasQuestSteps)
            {
                var original = await questRepository.GetQuestStepAsync(questStepDto.QuestStepKey);
                if (original == null)
                {
                    var maxSortOrder = await questRepository.GetMaxSortOrderAsync(questStepDto.QuestKey);
                    entity.SortOrder = maxSortOrder + 1;
                }
                else
                {
                    entity.SortOrder = original.SortOrder;
                }
            }
            else
            {
                entity.SortOrder = 0;
            }

            questRepository.Save(entity, ctx => ctx.QuestStep, qs => qs.QuestStepKey == questStepDto.QuestStepKey);
        }
    }
}