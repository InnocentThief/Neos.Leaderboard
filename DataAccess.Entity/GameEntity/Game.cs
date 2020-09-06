using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.GameEntity
{
    public class Game
    {
        /// <summary>
        /// Gets or sets the unique identifier of the game.
        /// </summary>
        [Key]
        public Guid GameKey { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associeted account.
        /// </summary>
        public Guid AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the game.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Name { get; set; }
    }
}