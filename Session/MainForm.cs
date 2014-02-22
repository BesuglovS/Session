using Schedule.DomainClasses.Main;
using Schedule.Repositories;
using Session.Forms;
using Session.Repositories;
using Session.View;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Session.wnu;
using Session.wnu.MySQLDomainClasses;
using Word = Microsoft.Office.Interop.Word;
using System.Globalization;
using Microsoft.Office.Core;

namespace Session
{
    public partial class MainForm : Form
    {
        private readonly SessionRepository _repo;
        private readonly ScheduleRepository _sRepo;

        public enum DataViews
        {
            ExamsView
        }

        public MainForm()
        {
            InitializeComponent();

            _repo = new SessionRepository();
            _sRepo = new ScheduleRepository();
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            FillLists();
        }

        private void FillLists()
        {
            var discIds = _repo
                .GetAllExams()
                .Select(e => e.DisciplineId)
                .ToList();

            var groupList = new List<StudentGroup>();

            foreach (var discId in discIds)
            {
                groupList.Add(_sRepo.GetDiscipline(discId).StudentGroup);
            }

            groupList = groupList
                .GroupBy(x => x.StudentGroupId).Select(y => y.First())
                .ToList();

            groupList = groupList
                .OrderBy(g => g.Name)
                .ToList();

            groupBox.ValueMember = "StudentGroupId";
            groupBox.DisplayMember = "Name";
            groupBox.DataSource = groupList;            
        }

        private void BigRedButton_Click(object sender, EventArgs e)
        {
            _repo.FillExamListFromSchedule(_sRepo);           

            var eprst = 999;
        }

        private void showAll_Click(object sender, EventArgs e)
        {
            var exams = _repo
                .GetAllExams()                                
                .OrderBy(ex => ex.ConsultationDateTime)
                .ToList();

            var ExamViewList = ExamView.FromExamList(_sRepo, exams);

            examsView.DataSource = ExamViewList;

            TuneDataView(examsView, DataViews.ExamsView);            
        }

        private void TuneDataView(DataGridView view, DataViews viewType)
        {
            switch (viewType)
            {
                case DataViews.ExamsView:
                    // ExamId
                    view.Columns["ExamId"].Visible = false;
                    view.Columns["ExamId"].Width = 0;

                    // GroupName
                    view.Columns["GroupName"].Width = 100;
                    view.Columns["GroupName"].HeaderText = "Группа";

                    // DisciplineName
                    view.Columns["DisciplineName"].Width = 400;
                    view.Columns["DisciplineName"].HeaderText = "Дисциплина";

                    // ConsultationDateTime
                    view.Columns["ConsultationDateTime"].Width = 100;
                    view.Columns["ConsultationDateTime"].HeaderText = "Дата/Время консультации";

                    // ConsultationAuditoriumId
                    view.Columns["ConsultationAuditorium"].Width = 100;
                    view.Columns["ConsultationAuditorium"].HeaderText = "Аудитория консультации";


                    // ExamDateTime
                    view.Columns["ExamDateTime"].Width = 100;
                    view.Columns["ExamDateTime"].HeaderText = "Дата/Время экзамена";

                    // ExamAuditoriumId
                    view.Columns["ExamAuditorium"].Width = 100;
                    view.Columns["ExamAuditorium"].HeaderText = "Аудитория экзамена";
                    break;
            }
            
        }

        private void groupBox_SelectedIndexChanged(object sender, EventArgs e)
        {                        
            var groupExams = _repo
                .GetGroupActiveExams(_sRepo, (int)groupBox.SelectedValue, false)
                .ToList();

            var ExamViewList = ExamView.FromExamList(_sRepo, groupExams);

            examsView.DataSource = ExamViewList;

            TuneDataView(examsView, DataViews.ExamsView);
        }

        private void examsView_DoubleClick(object sender, EventArgs e)
        {
            if (examsView.SelectedCells.Count == 0)
                return;

            var updateIndex = examsView.SelectedCells[0].RowIndex;

            var examToUpdateId = ((List<ExamView>)examsView.DataSource)[updateIndex].ExamId;

            var updateForm = new ExamProperties(_sRepo, _repo, examToUpdateId, Session.Forms.ExamProperties.ExamPropertiesMode.Edit);
            updateForm.ShowDialog();
        }

        private void UploadClick(object sender, EventArgs e)
        {
            string result;
            
            var jsonSerializer = new System.Web.Script.Serialization.JavaScriptSerializer();

            var mySQLExams = MySQLExam.FromExamList(_repo.GetAllExamRecords());
            var wud = new WnuUploadData { tableSelector = "exams", data = jsonSerializer.Serialize(mySQLExams) };
            string json = jsonSerializer.Serialize(wud);
            WnuUpload.UploadTableData(json);

            var MySQLlogEvents = MySQLExamLogEvent.FromLogEventList(_repo.GetAllLogEvents());
            wud = new WnuUploadData { tableSelector = "examsLogEvents", data = jsonSerializer.Serialize(MySQLlogEvents)};
            json = jsonSerializer.Serialize(wud);
            result = WnuUpload.UploadTableData(json);
        }

        private void WordExport_Click(object sender, EventArgs e)
        {
            SaveAsWordDocument();
        }
        
        private void SaveAsWordDocument()
        {
            DateTime beginSessionDate, endSessionDate;
            DetectSessionDates(out beginSessionDate, out endSessionDate);
            

            object oMissing = System.Reflection.Missing.Value;
            object oEndOfDoc = "\\endofdoc"; /* \endofdoc is a predefined bookmark */

            //Start Word and create a new document.
            
            Word._Application oWord = new Word.Application();
            oWord.Visible = true;
            Word._Document oDoc =
                oWord.Documents.Add(ref oMissing, ref oMissing, ref oMissing, ref oMissing);

            oDoc.PageSetup.TopMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.BottomMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.LeftMargin = oWord.CentimetersToPoints(1);
            oDoc.PageSetup.RightMargin = oWord.CentimetersToPoints(1);

            var faculties = _sRepo.GetAllFaculties();

            for (int facCounter = 0; facCounter < Constants.Constants.facultyGroups.Keys.Count; facCounter++)
            {
                var groupIds = new List<int>();

                foreach (var group in Constants.Constants.facultyGroups.ElementAt(facCounter).Value)
                {
                    var groupId = _sRepo.FindStudentGroup(group);
                    if (groupId != null)
                    {
                        groupIds.Add(groupId.StudentGroupId);
                    }
                }

                var facultyExams = _repo.GetFacultyExams(_sRepo, groupIds);

                facultyExams = facultyExams.OrderBy(fe => fe.Key).ToDictionary((keyItem) => keyItem.Key, (valueItem) => valueItem.Value);
                
                Word.Paragraph oPara1 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara1.Range.Font.Size = 24;
                oPara1.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara1.Range.Text = "Расписание";
                oPara1.Format.SpaceAfter = 0;
                oPara1.Range.InsertParagraphAfter();

                oPara1 = oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara1.Range.Font.Size = 14;
                oPara1.Format.SpaceAfter = 0;
                if (beginSessionDate.Month < 3)
                {
                    var startYear = beginSessionDate.Year - 1;
                    oPara1.Range.Text = "зимней сессии " + startYear + "-" + (startYear+1) + " учебного года" +
                        Environment.NewLine + Constants.Constants.facultyTitles[facCounter];
                }
                else
                {
                    var startYear = beginSessionDate.Year - 1;
                    oPara1.Range.Text = "летней сессии " + startYear + "-" + (startYear + 1) + " учебного года" +
                        Environment.NewLine + faculties[facCounter].Name;
                }
                oPara1.Range.InsertParagraphAfter();

                Word.Shape signBox = oDoc.Shapes
                    .AddTextbox(MsoTextOrientation.msoTextOrientationHorizontal, 350, 15, 200, 75, oPara1.Range);

                signBox.Line.Visible = MsoTriState.msoFalse;
                signBox.TextFrame.ContainingRange.ParagraphFormat.Alignment =
                    Word.WdParagraphAlignment.wdAlignParagraphRight;

                signBox.TextFrame.ContainingRange.InsertAfter("«УТВЕРЖДАЮ»");
                signBox.TextFrame.ContainingRange.InsertParagraphAfter();
                signBox.TextFrame.ContainingRange.InsertAfter("Проректор по учебной работе");
                signBox.TextFrame.ContainingRange.InsertParagraphAfter();
                signBox.TextFrame.ContainingRange.InsertAfter("____________  А.В. Синицкий");

                var groups = Constants.Constants.facultyGroups.ElementAt(facCounter).Value;
                
                Word.Table oTable;
                Word.Range wrdRng = oDoc.Bookmarks.get_Item(ref oEndOfDoc).Range;
                oTable = oDoc.Tables.Add(wrdRng, 1 + facultyExams.Keys.Count, 1 + groups.Count);

                //oTable.Rows(1).HeadingFormat = True;
                //oTable.ApplyStyleHeadingRows = True;
                oTable.Rows[1].HeadingFormat = -1;
                oTable.ApplyStyleHeadingRows = true;

                oTable.Borders.Enable = 1;
                
                for (int i = 1; i <= oTable.Rows.Count; i++)
                {
                    oTable.Rows[i].AllowBreakAcrossPages = (int)Microsoft.Office.Core.MsoTriState.msoFalse;
                }
                

                oTable.Cell(1, 1).Range.Text = "Дата";
                oTable.Cell(1, 1).Range.ParagraphFormat.Alignment =
                        Word.WdParagraphAlignment.wdAlignParagraphCenter;
                for (var column = 1; column <= groups.Count; column++)
                {
                    oTable.Cell(1, column + 1).Range.Text = groups[column - 1];
                    oTable.Cell(1, column + 1).Range.ParagraphFormat.Alignment =
                        Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                for (var row = 2; row <= 1 + facultyExams.Keys.Count; row++)
                {
                    oTable.Cell(row, 1).Range.Text = facultyExams.Keys.ElementAt(row - 2).ToString("dd MMMM yyyy", CultureInfo.CreateSpecificCulture("ru-RU"));
                    oTable.Cell(row, 1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;
                    oTable.Cell(row, 1).Range.ParagraphFormat.Alignment =
                        Word.WdParagraphAlignment.wdAlignParagraphCenter;
                }

                DateTime currentDate;

                for (var row = 2; row <= 1 + facultyExams.Keys.Count; row++)
                {
                    currentDate = facultyExams.Keys.ElementAt(row-2);

                    for (var column = 1; column <= groups.Count; column++)
                    {
                        if (facultyExams.ContainsKey(currentDate))
                        {
                            if (facultyExams[currentDate].ContainsKey(groupIds[column - 1]))
                            {
                                var eventCount = facultyExams[currentDate][groupIds[column - 1]].Count;

                                oTable.Cell(row, column + 1).VerticalAlignment = Word.WdCellVerticalAlignment.wdCellAlignVerticalCenter;

                                var timeTable = oDoc.Tables.Add(oTable.Cell(row, column + 1).Range, 1, 1);
                                timeTable.AutoFitBehavior(Microsoft.Office.Interop.Word.WdAutoFitBehavior.wdAutoFitWindow);
                                if (eventCount > 1)
                                {
                                    for (int i = 1; i < eventCount; i++)
                                    {
                                        timeTable.Rows.Add();
                                    }
                                }

                                for (int i = 0; i < eventCount; i++)
                                {
                                    string cellText = "";

                                    var evt = facultyExams[currentDate][groupIds[column - 1]][i];

                                    // Консультация || Экзамен                                                                
                                    if (evt.IsExam)
                                    {
                                        cellText += "Экзамен";
                                    }
                                    else
                                    {
                                        cellText += "Консультация";
                                    }

                                    cellText += Environment.NewLine;
                                    cellText += evt.DisciplineName + Environment.NewLine;
                                    cellText += evt.TeacherFIO + Environment.NewLine;
                                    cellText += evt.Time.ToString("H:mm") + Environment.NewLine;
                                    cellText += evt.Auditorium;

                                    oPara1 = oDoc.Content.Paragraphs.Add(timeTable.Cell(i+1, 1).Range);
                                    oPara1.Range.Font.Size = 10;
                                    oPara1.Format.SpaceAfter = 0;
                                    oPara1.Range.Text = cellText;

                                    if (i != eventCount - 1)
                                    {
                                        timeTable.Cell(i+1, 1).Borders[Microsoft.Office.Interop.Word.WdBorderType.wdBorderBottom].Visible = true;
                                    }
                                }
                            }
                        }
                    }

                    
                }


                oTable.Columns[1].Width = oWord.CentimetersToPoints(3);
                for (int i = 0; i < groups.Count; i++)
                {
                    oTable.Columns[i + 2].Width = oWord.CentimetersToPoints(16 / groups.Count);
                }

                oTable.Rows.Alignment = Word.WdRowAlignment.wdAlignRowCenter;

                Word.Paragraph oPara2 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara2.Range.Font.Size = 12;
                oPara2.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara2.Range.Text = "";
                oPara2.Format.SpaceAfter = 0;
                oPara2.Range.InsertParagraphAfter();

                oPara2 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara2.Range.Font.Size = 12;
                oPara2.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara2.Range.Text = "Начальник учебного отдела\t\t" + "_________________  " + Constants.Constants.UchOtdHead;
                oPara2.Format.SpaceAfter = 0;
                oPara2.Range.InsertParagraphAfter();
                oPara2.Range.InsertParagraphAfter();

                oPara2 =
                    oDoc.Content.Paragraphs.Add(ref oMissing);
                oPara2.Range.Font.Size = 12;
                oPara2.Format.LineSpacing = oWord.LinesToPoints(1);
                oPara2.Range.Text = "Декан " + Constants.Constants.facultyTitles[facCounter] + "\t\t_________________  "
                    + Constants.Constants.HeadsOfFaculties.ElementAt(facCounter).Value;
                oPara2.Format.SpaceAfter = 0;
                oPara2.Range.InsertParagraphAfter();
                oPara2.Range.InsertParagraphAfter();


                if (facCounter != Constants.Constants.facultyGroups.Keys.Count - 1)
                {
                    oDoc.Words.Last.InsertBreak(Word.WdBreakType.wdPageBreak);
                }

                Application.DoEvents();

            }

            object fileName = Application.StartupPath + @"\Export2.docx";

            //oDoc.SaveAs(ref fileName);

            //oWord.Quit();

        }

        private void DetectSessionDates(out DateTime beginSessionDate, out DateTime endSessionDate)
        {
            var minConsDate = _repo.GetAllExams().Select(e => e.ConsultationDateTime).Min();
            var minExamDate = _repo.GetAllExams().Select(e => e.ExamDateTime).Min();

            beginSessionDate = (minConsDate <= minExamDate) ? minConsDate : minExamDate;

            var maxConsDate = _repo.GetAllExams().Select(e => e.ConsultationDateTime).Max();
            var maxExamDate = _repo.GetAllExams().Select(e => e.ExamDateTime).Max();

            endSessionDate = (maxConsDate <= maxExamDate) ? maxConsDate : maxExamDate; 
        }
    }
}
