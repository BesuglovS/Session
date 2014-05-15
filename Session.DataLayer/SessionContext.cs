using Session.DomainClasses;
using System.Data.Entity;

namespace Session.DataLayer
{
    public class SessionContext : DbContext
    {
        public SessionContext()
            : base("data source=tcp:" + "127.0.0.1" + ",1433;Database=Session2DB;User ID = sa;Password = ghjuhfvvf;multipleactiveresultsets=True")
        {
        }

        public SessionContext(string connectionString)
            : base(connectionString)
        {
        }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<LogEvent> EventLog { get; set; }
    }
}
