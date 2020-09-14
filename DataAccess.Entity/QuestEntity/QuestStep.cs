using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DataAccess.Entity.QuestEntity
{
    /// <summary>
    /// Respresents a step of a quest.
    /// </summary>
    public sealed class QuestStep
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quest step.
        /// </summary>
        [Key]
        public Guid QuestStepKey { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated quest.
        /// </summary>
        public Guid QuestKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the quest step.
        /// </summary>
        [MaxLength(50)]
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description for the quest step.
        /// </summary>
        [MaxLength(4000)]
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the sort order for the quest step.
        /// </summary>
        public int SortOrder { get; set; }

        /// <summary>
        /// Gets or sets the related quest as defined by <see cref="QuestKey"/>.
        /// </summary>
        public Quest Quest { get; set; }

        /// <summary>
        /// Gets or sets a collection of quest step progression.
        /// </summary>
        public ICollection<QuestStepProgression> QuestStepProgressions { get; set; }
    }
}