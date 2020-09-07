using System;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.QuestEntity
{
    /// <summary>
    /// Represents a quest step progression.
    /// </summary>
    public class QuestStepProgression
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quest step progression.
        /// </summary>
        [Key]
        public Guid QuestStepProgressionKey { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the related quest step.
        /// </summary>
        public Guid QuestStepKey { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the related account.
        /// </summary>
        public Guid AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the date and time the quest step was resolved by the account.
        /// </summary>
        public DateTime ResolvedOn { get; set; }

        /// <summary>
        /// Gets or sets the related quest step as defined by <see cref="QuestStepKey"/>.
        /// </summary>
        public QuestStep QuestStep { get; set; }
    }
}