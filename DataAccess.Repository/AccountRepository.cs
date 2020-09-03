using DataAccess.Entity.AccountEntity;
using DataAccess.Model.Contexts;
using Framework.Security;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    /// <summary>
    /// Provides account based data access.
    /// </summary>
    public sealed class AccountRepository : BaseRepository<AccountContext>
    {
        /// <summary>
        /// Initializes a new <see cref="AccountRepository"/>.
        /// </summary>
        /// <param name="configuration">Provides access to configuration data.</param>
        public AccountRepository(IConfiguration configuration) : base(configuration)
        {

        }

        /// <summary>
        /// Creates the corresponding database context.
        /// </summary>
        /// <returns>The corresponding database context.</returns>
        protected override AccountContext GetDatabaseContext()
        {
            return new AccountContext(Configuration);
        }

        /// <summary>
        /// Retrieves the account entity for the given username.
        /// </summary>
        /// <param name="username">Username for which to get the account.</param>
        /// <returns></returns>
        public async Task<Account> GetAccountAsync(string username)
        {
            using var context = GetDatabaseContext();
            return await context.Account
                .AsNoTracking()
                .SingleOrDefaultAsync(a => a.Username == username);
        }

        /// <summary>
        /// Creates a new account if no account exists with the given username.
        /// </summary>
        /// <param name="username"></param>
        /// <returns></returns>
        public async Task<Account> CreateAccountAsync(string username, string password)
        {
            using var context = GetDatabaseContext();
            var original = await context.Account
                .SingleOrDefaultAsync(a => a.Username == username);

            if (original == null)
            {
                // Compute password hash and salt value
                var credentials = PasswordSecurity.CreateSecurityCredentials(password);

                original = new Account
                {
                    AccountKey = Guid.NewGuid(),
                    Username = username,
                    PasswordHash = credentials.PasswordHash,
                    Salt = credentials.SaltValue
                };
                context.Add(original);
                await context.SaveChangesAsync();
            }

            return original;
        }
    }
}