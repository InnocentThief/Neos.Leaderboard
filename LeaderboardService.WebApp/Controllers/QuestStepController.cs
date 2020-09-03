using DataTransfer.Dto.Dtos;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace LeaderboardService.WebApp.Controllers
{
    /// <summary>
    /// Handles quest step related HTTP requests.
    /// </summary>
    [Route("api/queststeps")]
    [ApiController]
    public class QuestStepController : ControllerBase
    {
        /// <summary>
        /// Retrieves the quest step for the given key.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the guest step to get.</param>
        /// <returns>An awaitable task that returns the requested <see cref="QuestStepDto"/>.</returns>
        [HttpGet]
        [Route("{questStepKey}")]
        public async Task<ActionResult<QuestStepDto>> GetQuestStepAsync(Guid questStepKey)
        {
            if (questStepKey == Guid.Empty) return BadRequest();

            await Task.CompletedTask;
            return new QuestStepDto();
        }

        /// <summary>
        /// Create a new quest step.
        /// </summary>
        /// <param name="questStepDto">The quest step to create.</param>
        /// <returns>An awaitable task that create a new quest step and returns the <see cref="QuestStepDto"/>.</returns>
        [HttpPost]
        public async Task<ActionResult<QuestStepDto>> PostQuestStepAsync(QuestStepDto questStepDto)
        {
            if (questStepDto == null) throw new ArgumentNullException(nameof(questStepDto));
            if (questStepDto.QuestKey == Guid.Empty) return BadRequest();

            await Task.CompletedTask;
            return CreatedAtAction(nameof(GetQuestStepAsync), 1, new QuestStepDto());
        }

        /// <summary>
        /// Update the given quest step.
        /// </summary>
        /// <param name="questStepDto">The quest step to update.</param>
        /// <returns>An awaitable task that yields no return value.</returns>
        [HttpPut]
        public async Task<IActionResult> PutQuestStepAsync(QuestStepDto questStepDto)
        {
            if (questStepDto == null) throw new ArgumentNullException(nameof(questStepDto));
            if (questStepDto.QuestStepKey == Guid.Empty) return BadRequest();

            await Task.CompletedTask;
            return NoContent();
        }


    }
}