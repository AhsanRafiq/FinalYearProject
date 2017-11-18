using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class MidTermModal
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public string StartDay { get; set; }
        public string StartMonth { get; set; }
        public string StartYear { get; set; }
        public string Time { get; set; }

        public string[] Sections { get; set; }
        public string SectionNames { get; set; }
        public string SessionName { get; internal set; }
        public string SemesterName { get; internal set; }
        public string CourseName { get; internal set; }
    }
}