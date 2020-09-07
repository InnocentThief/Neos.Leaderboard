using DataAccess.Entity.AccountEntity;
using DataAccess.Repository;
using Framework.Security;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace LeaderboardService.Business.Domains
{
    /// <summary>
    /// Provides credential verification as required by the account controller.
    /// </summary>
    public sealed class AccountDomatin
    {
        private readonly AccountRepository accountRepository;

        /// <summary>
        /// Initializes a new <see cref="AccountDomatin"/>.
        /// </summary>
        /// <param name="configuration">Provides access to configuration data.</param>
        public AccountDomatin(IConfiguration configuration
        {
            accountRepository = new AccountRepository(configuration);
        }

        /// <summary>
        /// Verifies the provided credentials against the stored user password.
        /// </summary>
        /// <param name="username">The username.</param>
        /// <param name="password">The password in plaintext.</param>
        /// <returns>An awaitable task that returns the <see cref="Account"/>.</returns>
        /// <remarks>If the user does not exist, a new one will be created.</remarks>
        public async Task<Account> LoginAsync(string username, string password)
        {
            if (string.IsNullOrWhiteSpace(username)) throw new ArgumentNullException(nameof(username));
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentNullException(nameof(password));

            // Get user by username
            var account = await accountRepository.GetAccountAsync(username);

            if (account == null)
            {
                // Create new account
                account = await accountRepository.CreateAccountAsync(username, password);
            }
            else
            {
                // Check password
                var credentials = new SecurityCredentials
                {
                    PasswordHash = account.PasswordHash,
                    SaltValue = account.Salt
                };
                var comparisonResult = PasswordSecurity.VerifyPassword(credentials, password);
                if (!comparisonResult) return null;
            }
            return account;
        }
    }
}