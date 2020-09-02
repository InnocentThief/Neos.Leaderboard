using DataAccess.Entity.AccountEntity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace DataAccess.Model.Contexts
{
    /// <summary>
    /// Provides a session context with account related database tables.
    /// </summary>
    public sealed class AccountContext : BaseContext
    {
        public AccountContext(IConfiguration iConfig): base(iConfig)
        {

        }

        /// <summary>
        /// Represents tha account data table.
        /// </summary>
        public DbSet<Account> Account { get; set; }
    }
}