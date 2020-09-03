namespace DataTransfer.Dto.Dtos
{
    /// <summary>
    /// Contains the user login credentials in readable plaintext format.
    /// </summary>
    public class LoginDto
    {
        /// <summary>
        /// Gets or sets the user name.
        /// </summary>
        public string Username { get; set; }

        /// <summary>
        /// Gets or sets the password.
        /// </summary>
        public string Password { get; set; }
    }
}