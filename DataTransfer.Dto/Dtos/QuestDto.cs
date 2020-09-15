using System;

namespace DataTransfer.Dto.Dtos
{
    /// <summary>
    /// Used to transfer a quest between client and server.
    /// </summary>
    public class QuestDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the quest.
        /// </summary>
        public Guid QuestKey { get; set; }

        /// <summary>
        /// Gets or sets the unique identifier of the associated account.
        /// </summary>
        public Guid AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the name of the quest.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description of the quest.
        /// </summary>
        public string Description { get; set; }
    }
}