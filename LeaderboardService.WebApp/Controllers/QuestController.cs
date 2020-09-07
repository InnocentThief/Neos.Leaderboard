using DataTransfer.Dto.Dtos;
using LeaderboardService.Business.Domains;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
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
        #region Private fields

        private readonly QuestDomain questDomain;
        private readonly ILogger logger;

        #endregion

        /// <summary>
        /// Initializes a new <see cref="QuestController"/>.
        /// </summary>
        public QuestController(IConfiguration configuration, ILogger<QuestController> logger)
        {
            questDomain = new QuestDomain(configuration);
            this.logger = logger;
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

            var quest = await questDomain.GetQuestAsync(questKey);
            return quest;
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

            try
            {
                var questSteps = await questDomain.GetQuestStepsAsync(questKey);
                return questSteps.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Retrieves the leader board for the given quest key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest for which to get the leader board.</param>
        /// <returns>An awaitable task that returns a collection of <see cref="QuestLeaderboardEntryDto"/>.</returns>
        [HttpGet]
        [Route("{questKey}/leaderboard")]
        public async Task<ActionResult<IEnumerable<QuestLeaderboardEntryDto>>> GetLeaderboardAsync(Guid questKey)
        {
            if (questKey == Guid.Empty) return BadRequest();

            try
            {
                var leaderboardEntry = await questDomain.GetLeaderboardAsync(questKey);
                return leaderboardEntry.ToList();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Creates a new quest.
        /// </summary>
        /// <param name="questDto">The quest to create.</param>
        /// <returns>An awaitable task that creates a new quest and returns the <see cref="QuestDto"/>/>.</returns>
        [HttpPost]
        public async Task<ActionResult> PostQuestAsync(QuestDto questDto)
        {
            if (questDto == null) throw new ArgumentNullException(nameof(questDto));
            if (questDto.AccountKey == Guid.Empty) return BadRequest();

            try
            {
                questDomain.SaveQuest(questDto);
                await Task.CompletedTask;
                return Ok();
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        /// <summary>
        /// Deletes the quest with the given key.
        /// </summary>
        /// <param name="questKey">Unique identifier of the quest to delete.</param>
        /// <returns>An awaitable task that yields no return value.</returns>
        [HttpDelete]
        public async Task<ActionResult> DeleteQuestAsync(Guid questKey)
        {
            if (questKey == Guid.Empty) throw new ArgumentNullException(nameof(questKey));

            try
            {
                questDomain.DeleteQuest(questKey);
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