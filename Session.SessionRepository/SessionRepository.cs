using Schedule.Repositories;
using Session.DataLayer;
using Session.DataLayer.Migrations;
using Session.DomainClasses;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Repositories
{
    public class SessionRepository : IDisposable
    {
        public string ConnectionString { get; set; }

        public SessionRepository(string connectionString)
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SessionContext, Configuration>());

            ConnectionString = connectionString;
        }

        public void ChangeConnectionString(string connectionString)
        {
            ConnectionString = connectionString;
        }

        public void CreateDB()
        {
            using (var context = new SessionContext(ConnectionString))
            {
                if (!context.Database.Exists())
                {
                    ((IObjectContextAdapter)context).ObjectContext.CreateDatabase();
                }
            }
        }

        public void RecreateDB()
        {
            using (var context = new SessionContext(ConnectionString))
            {
                context.Database.Delete();
                context.Database.CreateIfNotExists();
            }
        }

        private void Dispose(bool b)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                context.Dispose();
            }
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void ClearAllExams()
        {
            using (var context = new SessionContext(ConnectionString))
            {
                var examIds = context.Exams.Select(e => e.ExamId).ToList();

                foreach (var examId in examIds)
                {
                    RemoveExam(examId);
                }
            }
        }

        public void ClearExamLogs()
        {
            using (var context = new SessionContext(ConnectionString))
            {
                var logIds = context.EventLog.Select(le => le.LogEventId).ToList();

                foreach (var logId in logIds)
                {
                    RemoveLogEvent(logId);
                }
            }
        }

        private void RemoveLogEvent(int logEventId)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                var logEvent = context.EventLog.FirstOrDefault(le => le.LogEventId == logEventId);

                context.EventLog.Remove(logEvent);
                context.SaveChanges();
            }
        }

        private LogEvent GetLogEvent(int logEventId)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                return context
                    .EventLog
                    .Include(e => e.OldExam)
                    .Include(e => e.NewExam)
                    .FirstOrDefault(le => le.LogEventId == logEventId);
            }
        }


        public int GetTotalExamsCount()
        {
            using (var context = new SessionContext(ConnectionString))
            {
                return context
                    .Exams
                    .Count(e => e.IsActive);
            }
        }

        public List<Exam> GetAllExamRecords()
        {
            using (var context = new SessionContext(ConnectionString))
            {
                return context
                    .Exams
                    .ToList();
            }
        }

        public List<Exam> GetAllExams()
        {
            using (var context = new SessionContext(ConnectionString))
            {
                return context
                    .Exams
                    .Where(e => e.IsActive)
                    .ToList();
            }
        }

        public List<Exam> GetFiltredExams(Func<Exam, bool> condition)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                return context.Exams.ToList().Where(condition).ToList();
            }
        }

        public Exam GetFirstFiltredExam(Func<Exam, bool> condition)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                return context.Exams.ToList().FirstOrDefault(condition);
            }
        }

        public Exam GetExam(int examId)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                return context.Exams.FirstOrDefault(a => a.ExamId == examId);
            }
        }
        
        public void AddExam(Exam exam)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                exam.ExamId = 0;

                context.Exams.Add(exam);
                context.SaveChanges();
            }
        }

        public void UpdateExam(Exam exam)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                var oldExam = context.Exams.FirstOrDefault(e => e.ExamId == exam.ExamId);
                oldExam.IsActive = false;

                exam.ExamId = 0;

                context.Exams.Add(exam);
                context.SaveChanges();

                var logEntry = new LogEvent() { OldExam = oldExam, NewExam = exam, DateTime = DateTime.Now };

                context.EventLog.Add(logEntry);
                context.SaveChanges();
            }
        }

        public void UpdateExamWOLog(Exam exam)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                var curExam = GetExam(exam.ExamId);

                curExam.ConsultationAuditoriumId = exam.ConsultationAuditoriumId;
                curExam.ConsultationDateTime = exam.ConsultationDateTime;
                curExam.DisciplineId = exam.DisciplineId;
                curExam.ExamAuditoriumId = exam.ExamAuditoriumId;
                curExam.ExamDateTime = exam.ExamDateTime;
                curExam.ExamId = exam.ExamId;
                curExam.IsActive = exam.IsActive;

                context.SaveChanges();
            }
        }

        public void RemoveExam(int examId)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                var exam = context.Exams.FirstOrDefault(e => e.ExamId == examId);

                context.Exams.Remove(exam);
                try
                {
                    context.SaveChanges();
                }
                catch (Exception e)
                {
                    throw;
                }
            }
            
        }

        public void AddExamsRange(IEnumerable<Exam> examList)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                foreach (var exam in examList)
                {
                    exam.ExamId = 0;
                    context.Exams.Add(exam);
                }

                context.SaveChanges();
            }
        }

        public void FillExamListFromSchedule(ScheduleRepository _sRepo)
        {
            ClearExamLogs();

            ClearAllExams();
            
            var examDiscs = _sRepo
                .GetFiltredDisciplines(d => d.Attestation == 2 || d.Attestation == 3)
                .ToList();

            foreach (var disc in examDiscs)
            {
                AddExam(new Exam() { 
                    DisciplineId = disc.DisciplineId, 
                    IsActive = true, 
                    ConsultationDateTime = Constants.Constants.DefaultEmptyDateForEvent, 
                    ExamDateTime = Constants.Constants.DefaultEmptyDateForEvent
                });
            }
        }

        public List<Exam> GetGroupActiveExams(ScheduleRepository _sRepo, int groupId, bool limitToExactGroup = true)
        {
            List<int> discIds;

            if (limitToExactGroup)
            {
                discIds = _sRepo
                    .GetFiltredDisciplines(d => d.StudentGroup.StudentGroupId == groupId && (d.Attestation == 2 || d.Attestation == 3))
                    .Select(d => d.DisciplineId)
                    .Distinct()
                    .ToList();
            }
            else
            {
                var studentIds = _sRepo.GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == groupId)
                .ToList()
                .Select(stig => stig.Student.StudentId);

                var groupsListIds = _sRepo.GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                    .ToList()
                    .Select(stig => stig.StudentGroup.StudentGroupId);

                discIds = _sRepo
                    .GetFiltredDisciplines(d => groupsListIds.Contains(d.StudentGroup.StudentGroupId) && (d.Attestation == 2 || d.Attestation == 3))
                    .Select(d => d.DisciplineId)
                    .Distinct()
                    .ToList();
            }

            return GetFiltredExams(e => discIds.Contains(e.DisciplineId) && e.IsActive)
                .OrderBy(e => e.ConsultationDateTime)
                .ToList();
        }

        public List<LogEvent> GetAllLogEvents()
        {
            using (var context = new SessionContext(ConnectionString))
            {
                return context
                    .EventLog
                    .Include(e => e.OldExam)
                    .Include(e => e.NewExam)
                    .ToList();
            }
        }

        public void SaveChanges()
        {
            using (var context = new SessionContext(ConnectionString))
            {
                context.SaveChanges();
            }
        }

        public Dictionary<DateTime, Dictionary<int, List<SessionEvent>>> GetFacultyExams(ScheduleRepository _sRepo, List<int> groups)
        {
            // Дата - (id группы + список строк)
            var result = new Dictionary<DateTime, Dictionary<int, List<SessionEvent>>>();

            for (int i = 0; i < groups.Count; i++)
            {
                var groupExams = GetGroupActiveExams(_sRepo, groups[i], false);

                foreach (var exam in groupExams)
                {
                    var examGroups = new List<int>();
                    var discipline = _sRepo.GetDiscipline(exam.DisciplineId);

                    string fio = "";
                    
                    var tfd = _sRepo.GetFirstFiltredTeacherForDiscipline(tefd => tefd.Discipline.DisciplineId == discipline.DisciplineId);
                    if (tfd != null)
                    {
                        fio = tfd.Teacher.FIO;
                    }

                    if (!groups.Contains(discipline.StudentGroup.StudentGroupId))
                    {
                        var studentIds = _sRepo.GetFiltredStudentsInGroups(sig => sig.StudentGroup.StudentGroupId == discipline.StudentGroup.StudentGroupId)
                            .ToList()
                            .Select(stig => stig.Student.StudentId);

                        var groupsListIds = _sRepo.GetFiltredStudentsInGroups(sig => studentIds.Contains(sig.Student.StudentId))
                            .ToList()
                            .Select(stig => stig.StudentGroup.StudentGroupId);

                        foreach (var group in groups)
                        {
                            if (groupsListIds.Contains(group))
                            {
                                examGroups.Add(group);
                            }
                        }
                    }
                    if (examGroups.Count == 0)
                    {
                        examGroups.Add(discipline.StudentGroup.StudentGroupId);
                    }

                    if (exam.ConsultationDateTime != Constants.Constants.DefaultEmptyDateForEvent)
                    {
                        if (!result.ContainsKey(exam.ConsultationDateTime.Date))
                        {
                            result.Add(exam.ConsultationDateTime.Date, new Dictionary<int, List<SessionEvent>>());
                            foreach (var groupId in examGroups)
                            {
                                result[exam.ConsultationDateTime.Date].Add(groupId, new List<SessionEvent>());
                            }                            
                        }
                        foreach (var groupId in examGroups)
                        {
                            if (!result[exam.ConsultationDateTime.Date].ContainsKey(groupId))
                            {
                                result[exam.ConsultationDateTime.Date].Add(groupId, new List<SessionEvent>());
                            }
                        }

                        var consAud = _sRepo.GetAuditorium(exam.ConsultationAuditoriumId);
                        string consAudName = "";
                        if (consAud != null)
                        {
                            consAudName = consAud.Name;
                        }

                        foreach (var groupId in examGroups)
                        {
                            if (groupId == groups[i])
                            {
                                result[exam.ConsultationDateTime.Date][groupId].Add(new SessionEvent()
                                {
                                    IsExam = false,
                                    DisciplineName = discipline.Name,
                                    TeacherFIO = fio,
                                    Time = exam.ConsultationDateTime,
                                    Auditorium = consAudName
                                });
                            }
                        }
                    }

                    if (exam.ExamDateTime != Constants.Constants.DefaultEmptyDateForEvent)
                    {
                        if (!result.ContainsKey(exam.ExamDateTime.Date))
                        {
                            result.Add(exam.ExamDateTime.Date, new Dictionary<int, List<SessionEvent>>());
                            foreach (var groupId in examGroups)
                            {
                                result[exam.ExamDateTime.Date].Add(groupId, new List<SessionEvent>());
                            }
                        }
                        foreach (var groupId in examGroups)
                        {
                            if (!result[exam.ExamDateTime.Date].ContainsKey(groupId))
                            {
                                result[exam.ExamDateTime.Date].Add(groupId, new List<SessionEvent>());
                            }
                        }

                        var examAud = _sRepo.GetAuditorium(exam.ExamAuditoriumId);
                        string examAudName = "";
                        if (examAud != null)
                        {
                            examAudName = examAud.Name;
                        }

                        foreach (var groupId in examGroups)
                        {
                            if (groupId == groups[i])
                            {
                                result[exam.ExamDateTime.Date][groupId].Add(new SessionEvent()
                                {
                                    IsExam = true,
                                    DisciplineName = discipline.Name,
                                    TeacherFIO = fio,
                                    Time = exam.ExamDateTime,
                                    Auditorium = examAudName
                                });
                            }
                        }
                    }
                }
            }

            return result;
        }

        public void BackupDB(string filename)
        {
            using (var context = new SessionContext(ConnectionString))
            {
                var dbName = ExtractDBName(context.Database.Connection.ConnectionString);

                if (dbName == "")
                {
                    return;
                }

                var backupSQL = "BACKUP DATABASE " + dbName + " TO DISK = '" + filename + "' WITH FORMAT, MEDIANAME='" + dbName + "'";

                ExecuteQuery(backupSQL);
            }
        }

        public string ExtractDBName(string connectionString)
        {
            int startIndex = connectionString.IndexOf("Database=") + 9;

            if (startIndex == -1)
            {
                return "";
            }

            int endIndex = -1;
            if (startIndex != 0)
            {
                endIndex = connectionString.IndexOf(';', startIndex);
            }

            return connectionString.Substring(startIndex, endIndex - startIndex);
        }

        private void ExecuteQuery(string SQLQuery)
        {
            SqlConnection sqlConnection = new SqlConnection(ConnectionString);                            

            SqlCommand cmd = new SqlCommand();
            cmd.CommandText = SQLQuery;
            cmd.CommandType = CommandType.Text;
            cmd.Connection = sqlConnection;

            sqlConnection.Open();

            cmd.ExecuteNonQuery();

            sqlConnection.Close();
            
        }

        public void RestoreDB(string filename)
        {
            var restoreSQL = "use master; RESTORE DATABASE Session2DB FROM DISK = '" + filename + "' WITH REPLACE";
            ExecuteQuery(restoreSQL);
        }
    }
}
