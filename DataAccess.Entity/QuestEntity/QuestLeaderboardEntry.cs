namespace DataAccess.Entity.QuestEntity
{
    /// <summary>
    /// Represents the quest progression per user.
    /// </summary>
    public class QuestLeaderboardEntry
    {
        /// <summary>
        /// Gets or sets the username
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the amount of quest steps done by the user.
        /// </summary>
        public int QuestStepsDone { get; set; }
    }
}