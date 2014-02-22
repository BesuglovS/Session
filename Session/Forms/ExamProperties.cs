using Schedule.Repositories;
using Session.DomainClasses;
using Session.Repositories;
using System;
using System.Windows.Forms;


namespace Session.Forms
{
    public partial class ExamProperties : Form
    {
        public enum ExamPropertiesMode
        {
            New,
            Edit
        }

        private readonly SessionRepository _repo;
        private readonly ScheduleRepository _sRepo;
        private readonly int _examId;
        private Exam _exam;
        private readonly ExamPropertiesMode _mode;

        public ExamProperties(ScheduleRepository sRepo, SessionRepository repo, int examToUpdateId, ExamPropertiesMode mode)
        {
            InitializeComponent();

            _sRepo = sRepo;
            _repo = repo;
            _examId = examToUpdateId;
            _mode = mode;

            if (_mode == ExamPropertiesMode.Edit)
            {
                _exam = _repo.GetExam(_examId);
            }

            if (_mode == ExamPropertiesMode.New)
            {
                _exam = new Exam();
            }
        }

        private void SaveWOLog_Click(object sender, EventArgs e)
        {
            _exam.ConsultationDateTime = ConsDate.Value;
            var consAud = _sRepo.FindAuditorium(ConsAudBox.Text);
            if (consAud != null)
            {
                _exam.ConsultationAuditoriumId = consAud.AuditoriumId;
            }

            _exam.ExamDateTime = ExamDate.Value;
            var examAud = _sRepo.FindAuditorium(ExamAudBox.Text);
            if (examAud != null)
            {
                _exam.ExamAuditoriumId = examAud.AuditoriumId;
            }            

            Close();
        }

        private void ExamProperties_Load(object sender, EventArgs e)
        {

            if (_mode == ExamPropertiesMode.Edit)
            {
                var disc = _sRepo.GetDiscipline(_exam.DisciplineId);
                var teacher = _sRepo
                    .GetFirstFiltredTeacherForDiscipline(tfd => tfd.Discipline.DisciplineId == disc.DisciplineId)
                    .Teacher;

                discipline.Text  = disc.StudentGroup.Name + Environment.NewLine;
                discipline.Text += disc.Name + Environment.NewLine;
                discipline.Text += teacher.FIO + Environment.NewLine;
                discipline.Text += disc.AuditoriumHours + " @ " + disc.LectureHours + " / "  + disc.PracticalHours + Environment.NewLine;
                discipline.Text += disc.Name;

                if (_exam.ConsultationDateTime == Constants.Constants.DefaultEmptyDateForEvent)
                {
                    ConsDate.Value = Constants.Constants.DefaultEditDate;
                }
                else
                {
                    ConsDate.Value = _exam.ConsultationDateTime;
                }
                var cAud = _sRepo.GetAuditorium(_exam.ConsultationAuditoriumId);
                if (cAud != null)
                {
                    ConsAudBox.Text = cAud.Name;
                }
                else
                {
                    ConsAudBox.Text = "";
                }

                if (_exam.ExamDateTime == Constants.Constants.DefaultEmptyDateForEvent)
                {
                    ExamDate.Value = Constants.Constants.DefaultEditDate;   
                }
                else
                {
                    ExamDate.Value = _exam.ExamDateTime;
                }
                var eAud = _sRepo.GetAuditorium(_exam.ExamAuditoriumId);
                if (eAud != null)
                {
                    ExamAudBox.Text = eAud.Name;
                }
                else
                {
                    ExamAudBox.Text = "";
                }
            }
        }

        private void Save_Click(object sender, EventArgs e)
        {
            var newExam = new Exam() { ExamId = _exam.ExamId, DisciplineId = _exam.DisciplineId, IsActive = true };

            newExam.ConsultationDateTime = ConsDate.Value;
            var consAud = _sRepo.FindAuditorium(ConsAudBox.Text);
            if (consAud != null)
            {
                newExam.ConsultationAuditoriumId = consAud.AuditoriumId;
            }

            newExam.ExamDateTime = ExamDate.Value;
            var examAud = _sRepo.FindAuditorium(ExamAudBox.Text);
            if (examAud != null)
            {
                newExam.ExamAuditoriumId = examAud.AuditoriumId;
            }

            _repo.UpdateExam(newExam);

            Close();
        }

        private void Cancel_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}
