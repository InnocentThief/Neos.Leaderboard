using DataTransfer.Dto.Dtos;
using LeaderboardService.Business.Domains;
using LeaderboardService.WebApp.Mock;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardService.WebApp.Controllers
{
    /// <summary>
    /// Handles quest related HTTP requests.
    /// </summary>
    [Route("api/quests")]
    [ApiController]
    public class QuestController : ControllerBase
    {
        private QuestDomain questDomain;

        /// <summary>
        /// Initializes a new <see cref="QuestController"/>.
        /// </summary>
        public QuestController(IConfiguration configuration)
        {
            questDomain = new QuestDomain(configuration);
        }

        /// <summary>
        /// Retrieves the quest with the given key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest to get.</param>
        /// <returns>An awaitable task that returns the requested <see cref="QuestDto"/>.</returns>
        [HttpGet]
        [Route("{questKey}")]
        public async Task<ActionResult<QuestDto>> GetQuestAsync(Guid questKey)
        {
            if (questKey == Guid.Empty) return BadRequest();

            await Task.CompletedTask;
            return new QuestDto();
        }

        /// <summary>
        /// Retrieves a list of quest steps for the given quest key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest related to the quest steps.</param>
        /// <returns>An awaitable task that returns a collection of <see cref="QuestStepDto"/>.</returns>
        [HttpGet]
        [Route("{questKey}/queststeps")]
        public async Task<ActionResult<IEnumerable<QuestStepDto>>> GetQuestStepsAsync(Guid questKey)
        {
            if (questKey == Guid.Empty) return BadRequest();

            var questSteps = Mocks.GetQuestSteps().ToList();

            await Task.CompletedTask;
            return questSteps;
        }

        /// <summary>
        /// Creates a new quest.
        /// </summary>
        /// <param name="questDto">The quest to create.</param>
        /// <returns>An awaitable task that creates a new quest and returns the <see cref="QuestDto"/>/>.</returns>
        [HttpPost]
        public async Task<ActionResult<QuestDto>> PostQuestAsync(QuestDto questDto)
        {
            if (questDto == null) throw new ArgumentNullException(nameof(questDto));
            if (questDto.AccountKey == Guid.Empty) return BadRequest();



            await Task.CompletedTask;
            return CreatedAtAction(nameof(GetQuestAsync), 1, new QuestDto());
        }

        /// <summary>
        /// Updates the given quest.
        /// </summary>
        /// <param name="questDto">The quest to update.</param>
        /// <returns>An awaitable task that yields no return value.</returns>
        [HttpPut]
        public async Task<IActionResult> PutQuestAsync(QuestDto questDto)
        {
            if (questDto == null) throw new ArgumentNullException(nameof(questDto));
            if (questDto.QuestKey == Guid.Empty) return BadRequest();

            await Task.CompletedTask;
            return NoContent();
        }

        /// <summary>
        /// Deletes the quest with the given key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest to delete.</param>
        /// <returns>An awaitable task that returns the dto of the deleted quest.</returns>
        [HttpDelete]
        public async Task<ActionResult<QuestDto>> DeleteQuestAsync(Guid questKey)
        {
            if (questKey == Guid.Empty) throw new ArgumentNullException(nameof(questKey));

            questDomain.DeleteQuest(questKey);

            await Task.CompletedTask;
            return new QuestDto();
        }
    }
}