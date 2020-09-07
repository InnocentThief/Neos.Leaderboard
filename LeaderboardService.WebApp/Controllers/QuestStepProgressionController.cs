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
    /// Handles quest step progression related HTTP requests.
    /// </summary>
    [Route("api/queststepprogressions")]
    [ApiController]
    public class QuestStepProgressionController : ControllerBase
    {
        #region Private fields

        private readonly QuestStepProgressionDomain questStepProgressionDomain;
        private readonly ILogger logger;

        #endregion

        /// <summary>
        /// Initializes a new <see cref="QuestStepProgressionController"/>.
        /// </summary>
        /// <param name="configuration">Provides access to configuration data.</param>
        /// <param name="logger">Provides access to logger handling.</param>
        public QuestStepProgressionController(IConfiguration configuration, ILogger<QuestStepProgressionController> logger)
        {
            questStepProgressionDomain = new QuestStepProgressionDomain(configuration);
            this.logger = logger;
        }

        /// <summary>
        /// Adds a new quest step progression.
        /// </summary>
        /// <returns>An awaitable task that returns true on progression.</returns>
        [HttpPost]
        public async Task<ActionResult<bool>> PostQuestStepProgression(QuestStepProgressionDto questStepProgressionDto)
        {
            if (questStepProgressionDto == null) return BadRequest();

            try
            {
                var resolved = await questStepProgressionDomain.ProgressAsync(questStepProgressionDto);
                return resolved;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}