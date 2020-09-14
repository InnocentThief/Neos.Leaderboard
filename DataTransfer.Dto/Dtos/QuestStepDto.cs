using System;

namespace DataTransfer.Dto.Dtos
{
    /// <summary>
    /// Used to transfer a quest step between client and server.
    /// </summary>
    public class QuestStepDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quest step.
        /// </summary>
        public Guid QuestStepKey { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated quest.
        /// </summary>
        public Guid QuestKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the quest step.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description for the quest step.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the sort order for the quest step.
        /// </summary>
        public string SortOrder { get; set; }

        /// <summary>
        /// Gets or sets whether the quest step can be reorderd.
        /// </summary>
        public bool CanReorder { get; set; }
    }
}