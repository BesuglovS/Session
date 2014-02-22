using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Session.Repositories
{
    public class SessionEvent
    {
        public bool IsExam { get; set; }
        public string DisciplineName { get; set; }
        public string TeacherFIO { get; set; }        
        public DateTime Time { get; set; }
        public string Auditorium { get; set; }
    }
}
