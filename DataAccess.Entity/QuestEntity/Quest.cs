using DataAccess.Entity.AccountEntity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.QuestEntity
{
    /// <summary>
    /// Respresents a quest created by an account with multiple steps.
    /// </summary>
    public sealed class Quest
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quest.
        /// </summary>
        [Key]
        public Guid QuestKey { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated account.
        /// </summary>
        public Guid AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the quest.
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the quest.
        /// </summary>
        [MaxLength(4000)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the related account as defined by <see cref="AccountKey"/>.
        /// </summary>
        public Account Account { get; set; }

        /// <summary>
        /// Gets or sets the quest related steps.
        /// </summary>
        public ICollection<QuestStep> QuestSteps { get; set; }
    }
}