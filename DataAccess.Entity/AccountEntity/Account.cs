using DataAccess.Entity.QuestEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.AccountEntity
{
    /// <summary>
    /// Respresents an account with multiple quests and games.
    /// </summary>
    public sealed class Account
    {
        /// <summary>
        /// Gets or sets the unique identifier of the account.
        /// </summary>
        [Key]
        public Guid AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the account.
        /// </summary>
        [MaxLength(100)]
        [Required]
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password hash.
        /// </summary>
        public byte[] PasswordHash { get; set; }

        /// <summary>
        /// Gets or sets the salt for the password.
        /// </summary>
        public byte[] Salt { get; set; }

        /// <summary>
        /// Gets or sets the account related quests.
        /// </summary>
        public ICollection<Quest> Quests { get; set; }
    }
}