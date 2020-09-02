using DataTransfer.Dto.Dtos;
using LeaderboardService.WebApp.Mock;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LeaderboardService.WebApp.Controllers
{
    /// <summary>
    /// Handles account related HTTPS requests.
    /// </summary>
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        /// <summary>
        /// Retrieves all games for a given user.
        /// </summary>
        /// <param name="username">Neos username.</param>
        /// <returns>An awaitable task that returns a collection of <see cref="GameDto"/>.</returns>
        [HttpGet]
        [Route("{username}/games")]
        public async Task<ActionResult<IEnumerable<GameDto>>> GetGamesAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return BadRequest();

            await Task.CompletedTask;
            return new List<GameDto>();
        }

        /// <summary>
        /// Retrieves all quests for the given user.
        /// </summary>
        /// <param name="username">Neos username.</param>
        /// <returns>An awaitable task that returns a collection of <see cref="QuestDto"/>.</returns>
        [HttpGet]
        [Route("{username}/quests")]
        public async Task<ActionResult<IEnumerable<QuestDto>>> GetQuestsAsync(string username)
        {
            if (string.IsNullOrWhiteSpace(username)) return BadRequest();

            var quests = Mocks.GetQuests().ToList();

            await Task.CompletedTask;
            return quests;
        }
    }
}