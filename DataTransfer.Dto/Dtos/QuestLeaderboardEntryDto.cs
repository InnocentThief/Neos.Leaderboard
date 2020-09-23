namespace DataTransfer.Dto.Dtos
{
    /// <summary>
    /// Used to transfer a quest leader board entry between client and server.
    /// </summary>
    public class QuestLeaderboardEntryDto
    {

        public string Position { get; set; }

        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the amount of quest steps done by the user.
        /// </summary>
        public string QuestStepsDone { get; set; }
    }
}