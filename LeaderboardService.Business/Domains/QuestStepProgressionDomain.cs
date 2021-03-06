﻿using DataAccess.Entity.QuestEntity;
using DataAccess.Repository;
using DataTransfer.Dto.Dtos;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardService.Business.Domains
{
    /// <summary>
    /// Provides ques step progression related data handling.
    /// </summary>
    public class QuestStepProgressionDomain
    {
        #region Private fields

        private readonly AccountRepository accountRepository;
        private readonly QuestRepository questRepository;
        private readonly QuestStepProgressionRepository questStepProgressionRepository;

        #endregion

        /// <summary>
        /// Initializes a new <see cref="QuestStepProgressionDomain"/>.
        /// </summary>
        /// <param name="configuration">Provides access to configuration data.</param>
        public QuestStepProgressionDomain(IConfiguration configuration)
        {
            accountRepository = new AccountRepository(configuration);
            questRepository = new QuestRepository(configuration);
            questStepProgressionRepository = new QuestStepProgressionRepository(configuration);
        }

        /// <summary>
        /// Adds a new progress entry to the quest step for the given account.
        /// </summary>
        /// <param name="questStepProgressionDto">Contains the quest step progression data.</param>
        /// <returns>An awaitable task that returns true if the quest step progression has been added.</returns>
        public async Task<bool> ProgressAsync(QuestStepProgressionDto questStepProgressionDto)
        {
            // Get quest steps
            var questStep = await questRepository.GetQuestStepAsync(questStepProgressionDto.QuestStepKey);
            if (questStep == null) return false;

            var existingQuestStepProgression = await questStepProgressionRepository.GetQuestStepProgressionAsync(questStepProgressionDto.QuestStepKey, questStepProgressionDto.Username);
            if (existingQuestStepProgression != null) return false;

            // Check if previous quest step is done
            if (questStep.SortOrder > 0)
            {
                var previousQuestStepDone = await questStepProgressionRepository.IsPreviousQuestStepDone(questStep.QuestKey, questStepProgressionDto.Username, questStep.SortOrder - 1);
                if (!previousQuestStepDone) return false;
            }

            // Add new quest step progression
            var questStepProgression = new QuestStepProgression
            {
                Username = questStepProgressionDto.Username,
                QuestStepKey = questStep.QuestStepKey,
                QuestStepProgressionKey = Guid.NewGuid(),
                ResolvedOn = DateTime.Now
            };
            questStepProgressionRepository.Save(questStepProgression, ctx => ctx.QuestStepProgression, qsp => qsp.QuestStepProgressionKey == questStepProgression.QuestStepProgressionKey);

            return true;
        }
    }
}