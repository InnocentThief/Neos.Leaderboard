using DataTransfer.Dto.Dtos;
using LeaderboardService.Business.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        #region Private fields

        private readonly QuestDomain questDomain;
        private readonly ILogger logger;

        #endregion

        public QuestStepController(IConfiguration configuration, ILogger<QuestStepController> logger)
        {
            questDomain = new QuestDomain(configuration);
            this.logger = logger;
        }

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
        /// Moves the quest step with the given key on place up (and changes the previous quest step.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the quest step.</param>
        /// <returns>An awaitable task that yields no retrun value.</returns>
        [HttpPost]
        [Route("{questStepKey}/MoveDown")]
        public async Task<ActionResult> MoveDownAsync(Guid questStepKey)
        {
            if (questStepKey == Guid.Empty) return BadRequest();

            try
            {
                await questDomain.MoveDownAsync(questStepKey);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Moves the quest step with the given key on place down (and changes the next quest step.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the quest step.</param>
        /// <returns>An awaitable task that yields no return value.</returns>
        [HttpPost]
        [Route("{questStepKey}/MoveUp")]
        public async Task<ActionResult> MoveUpAsync(Guid questStepKey)
        {
            if (questStepKey == Guid.Empty) return BadRequest();

            try
            {
                await questDomain.MoveUpAsync(questStepKey);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Create a new quest step.
        /// </summary>
        /// <param name="questStepDto">The quest step to create.</param>
        /// <returns>An awaitable task that create a new quest step and returns the <see cref="QuestStepDto"/>.</returns>
        [HttpPost]
        public async Task<ActionResult> PostQuestStepAsync(QuestStepDto questStepDto)
        {
            if (questStepDto == null) throw new ArgumentNullException(nameof(questStepDto));
            if (questStepDto.QuestKey == Guid.Empty) return BadRequest();

            try
            {
                await questDomain.SaveQuestStep(questStepDto);
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes the quest step with the given key.
        /// </summary>
        /// <param name="questStepKey">Unique identifier of the quest step to delete.</param>
        /// <returns>An awaitable task that yields no return value.</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteQuestStepAsync(Guid questStepKey)
        {
            if (questStepKey == Guid.Empty) throw new ArgumentNullException(nameof(questStepKey));

            try
            {
                questDomain.DeleteQuestStep(questStepKey);
                await Task.CompletedTask;
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}