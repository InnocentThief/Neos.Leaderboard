using DataAccess.Entity.AccountEntity;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.Model.Contexts
{
    /// <summary>
    /// Provides a session context with account related database tables.
    /// </summary>
    public sealed class AccountContext : BaseContext
    {
        /// <summary>
        /// Represents tha account data table.
        /// </summary>
        public DbSet<Account> Account { get; set; }
    }
}