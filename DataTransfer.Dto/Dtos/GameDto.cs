using System;

namespace DataTransfer.Dto.Dtos
{
    /// <summary>
    /// Used to transfer game data between client and server.
    /// </summary>
    public class GameDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the game.
        /// </summary>
        public Guid GameKey { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associeted account.
        /// </summary>
        public Guid AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        public string Name { get; set; }
    }
}