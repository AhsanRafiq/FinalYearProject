using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class CourseModal
    {

        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SemesterId { get; set; }
        public string SemesterNo { get; set; }

        public List<TeacherModal> Teachers { get; set; }
        public string CourseCode { get; set; }
        public string CourseName { get; set; }
        public int CreditHour { get; set; }
    }
}