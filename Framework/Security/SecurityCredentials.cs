namespace Framework.Security
{
    /// <summary>
    /// Encapsulates the user's security credentials to store in database.
    /// </summary>
    public sealed class SecurityCredentials
    {
        /// <summary>
        /// Gets or sets the salt value.
        /// </summary>
        public byte[] SaltValue { get; set; }

        /// <summary>
        /// Gets or sets the hashed password.
        /// </summary>
        public byte[] PasswordHash { get; set; }
    }
}