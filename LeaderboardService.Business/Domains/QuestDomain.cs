using DataAccess.Repository;
using DataTransfer.Dto.Dtos;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace LeaderboardService.Business.Domains
{
    public sealed class QuestDomain
    {
        private readonly QuestRepository questRepository;

        public QuestDomain(IConfiguration configuration)
        {
            questRepository = new QuestRepository(configuration);
        }

        //public async Task<QuestDto> GetQuestAsync(Guid questKey)
        //{

        //}
    }
}
