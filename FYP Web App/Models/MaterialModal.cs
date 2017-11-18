using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class MaterialModal
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string MaterialName { get; set; }
        public string MaterialDescription { get; set; }



        // For Getting Materials from DB//
        public string SessionName { get; set; }
        public string SemesterName { get; set; }
        public string CourseName { get; set; }
        public string TeacherName { get; set; }

    }
}