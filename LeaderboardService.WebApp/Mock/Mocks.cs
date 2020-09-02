using DataTransfer.Dto.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardService.WebApp.Mock
{
    public static class Mocks
    {
        /// <summary>
        /// Gets a list of quests.
        /// </summary>
        public static ICollection<QuestDto> GetQuests()
        {
            var retval = new List<QuestDto>();

            for (int i = 0; i < 10; i++)
            {
                retval.Add(new QuestDto
                {
                    AccountKey = Guid.NewGuid(),
                    Name = $"Quest {i}",
                    QuestKey = Guid.NewGuid()
                });
            }

            return retval;
        }

        /// <summary>
        /// Gets a list of quest steps.
        /// </summary>
        /// <returns></returns>
        public static ICollection<QuestStepDto> GetQuestSteps()
        {
            var retval = new List<QuestStepDto>();

            for (int i = 0; i < 40; i++)
            {
                retval.Add(new QuestStepDto
                {
                    Description = $"Description {i} Description {i} Description {i} Description {i} Description {i} Description {i} Description {i} ",
                    Name = $"Name {i}",
                    QuestKey = Guid.NewGuid(),
                    QuestStepKey = Guid.NewGuid(),
                    SortOrder = i
                });
            }

            return retval;
        }
    }
}