using DataAccess.Model.Contexts;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Text;

namespace DataAccess.Repository
{
    public sealed class QuestRepository : BaseRepository<QuestContext>
    {
        public QuestRepository(IConfiguration configuration) : base(configuration)
        {

        }

        protected override QuestContext GetDatabaseContext()
        {
            return new QuestContext(Configuration);
        }
    }
}
