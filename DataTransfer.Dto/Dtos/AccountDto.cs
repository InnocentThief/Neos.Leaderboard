using System;

namespace DataTransfer.Dto.Dtos
{
    /// <summary>
    /// Used to transfer account data between client and server.
    /// </summary>
    public class AccountDto
    {
        /// <summary>
        /// Gets or sets the unique identifier of the account.
        /// </summary>
        public Guid AccountKey { get; set; }

        /// <summary>
        /// Gets or sets the username of the account.
        /// </summary>
        public string Username { get; set; }
    }
}