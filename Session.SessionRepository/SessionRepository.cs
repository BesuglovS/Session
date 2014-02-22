using Schedule.Repositories;
using Session.DataLayer;
using Session.DataLayer.Migrations;
using Session.DomainClasses;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Repositories
{
    public class SessionRepository : IDisposable
    {
        private readonly SessionContext _context = new SessionContext();

        public SessionRepository()
        {
            Database.SetInitializer(new MigrateDatabaseToLatestVersion<SessionContext, Configuration>());
        }

        private void Dispose(bool b)
        {
            _context.Dispose();
        }

        public void Dispose()
        {
            Dispose(true);
        }

        public void ClearAllExams()
        {
            var examIds = _context.Exams.Select(e => e.ExamId).ToList();

            foreach (var examId in examIds)
            {
                RemoveExam(examId);
            }
        }

        public void ClearExamLogs()
        {
            var logIds = _context.EventLog.Select(le => le.LogEventId).ToList();

            foreach (var logId in logIds)
            {
                RemoveLogEvent(logId);
            }
        }

        private void RemoveLogEvent(int logEventId)
        {
            var logEvent = GetLogEvent(logEventId);

            _context.EventLog.Remove(logEvent);
            _context.SaveChanges();
        }

        private LogEvent GetLogEvent(int logEventId)
        {
            return _context.EventLog.FirstOrDefault(le => le.LogEventId == logEventId);
        }


        public int GetTotalExamsCount()
        {
            return _context
                .Exams
                .Count(e => e.IsActive);
        }

        public List<Exam> GetAllExamRecords()
        {
            return _context
                .Exams
                .ToList();
        }

        public List<Exam> GetAllExams()
        {
            return _context
                .Exams
                .Where(e => e.IsActive)
                .ToList();
        }

        public List<Exam> GetFiltredExams(Func<Exam, bool> condition)
        {
            return _context.Exams.ToList().Where(condition).ToList();
        }

        public Exam GetFirstFiltredExam(Func<Exam, bool> condition)
        {
            return _context.Exams.ToList().FirstOrDefault(condition);
        }

        public Exam GetExam(int examId)
        {
            return _context.Exams.FirstOrDefault(a => a.ExamId == examId);
        }
        
        public void AddExam(Exam exam)
        {
            exam.ExamId = 0;

            _context.Exams.Add(exam);
            _context.SaveChanges();
        }

        public void UpdateExam(Exam exam)
        {
            var oldExam = GetExam(exam.ExamId);
            oldExam.IsActive = false;
            
            exam.ExamId = 0;

            _context.Exams.Add(exam);
            _context.SaveChanges();

            var logEntry = new LogEvent() { OldExam = oldExam, NewExam = exam, DateTime = DateTime.Now };

            _context.EventLog.Add(logEntry);
            _context.SaveChanges();
        }

        public void UpdateExamWOLog(Exam exam)
        {
            var curExam = GetExam(exam.ExamId);

            curExam.ConsultationAuditoriumId = exam.ConsultationAuditoriumId;
            curExam.ConsultationDateTime = exam.ConsultationDateTime;
            curExam.DisciplineId = exam.DisciplineId;
            curExam.ExamAuditoriumId = exam.ExamAuditoriumId;
            curExam.ExamDateTime = exam.ExamDateTime;
            curExam.ExamId = exam.ExamId;
            curExam.IsActive = exam.IsActive;            

            _context.SaveChanges();
        }

        public void RemoveExam(int examId)
        {
            var exam = GetExam(examId);

            _context.Exams.Remove(exam);
            try
            {
                _context.SaveChanges();
            }
            catch (Exception e)
            {
                throw;
            }
            
        }

        public void AddExamsRange(IEnumerable<Exam> examList)
        {
            foreach (var exam in examList)
            {
                exam.ExamId = 0;
                _context.Exams.Add(exam);
            }

            _context.SaveChanges();
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
            return _context
                .EventLog
                .ToList();
        }

        public void SaveChanges()
        {
            _context.SaveChanges();
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
    }
}
