using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class PaperResultModal
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public int PaperId { get; set; }
        public int StudentId { get; set; }
        public int MarksObtained { get; set; }
        public int TotalMarks { get; set; }
        public string ResultSheet { get; set; }
  
        public string StudentRollNumber { get; set; }

    }
}