using System;

namespace DataTransfer.Dto.Dtos
{
    public class QuestStepProgressionDto
    {
        /// <summary>
        /// Gets or sets the neos username.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the quest step.
        /// </summary>
        public Guid QuestStepKey { get; set; }
    }
}