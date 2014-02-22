using Session.DomainClasses;
using System.Data.Entity;

namespace Session.DataLayer
{
    public class SessionContext : DbContext
    {
        public SessionContext()
            : base("Name=SessionConnection")
        {
        }

        public DbSet<Exam> Exams { get; set; }
        public DbSet<LogEvent> EventLog { get; set; }
    }
}
