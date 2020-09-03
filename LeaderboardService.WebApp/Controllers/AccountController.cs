﻿using DataTransfer.Dto.Converter;
using DataTransfer.Dto.Dtos;
using LeaderboardService.Business.Domains;
using LeaderboardService.WebApp.Mock;
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
    /// Handles account related HTTP requests.
    /// </summary>
    [Route("api/accounts")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        #region Private fields

        private readonly AccountDomatin accountDomain;
        private readonly ILogger logger;

        #endregion

        /// <summary>
        /// Initializes a new <see cref="AccountController"/>.
        /// </summary>
        /// <param name="configuration">Provides access to configuration data.</param>
        public AccountController(IConfiguration configuration, ILogger<AccountController> logger)
        {
            accountDomain = new AccountDomatin(configuration);
            this.logger = logger;
        }

        /// <summary>
        /// Tries to login.
        /// </summary>
        /// <param name="loginDto">Contains the user login credentials in readable plaintext format.</param>
        /// <returns>An awaitable task that returns an <see cref="AccountDto"/>.</returns>
        [HttpPost]
        [Route("")]
        public async Task<ActionResult<AccountDto>> LoginAsync(LoginDto loginDto)
        {
            try
            {
                if (loginDto == null) return BadRequest();

                var account = await accountDomain.LoginAsync(loginDto.Username, loginDto.Password);
                return account.ToDto(); ;
            }
            catch (Exception ex)
            {
                logger.LogError(ex.Message);
                return Unauthorized();
            }
        }

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