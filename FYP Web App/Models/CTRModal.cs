using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class CTRModal
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SemesterId { get; set; }
        public int SectionId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }





        public string CourseName { get; set; }
        public string EmployeeId { get; set; }
        public string TeacherName { get; set; }
    }
}