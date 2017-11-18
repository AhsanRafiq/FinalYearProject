using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class StudentModal
    {
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SemesterId { get; set; }

        public int SectionId { get; set; }
        public string RollNumber {get; set;}

        public string Name { get; set; }

        public string FatherName { get; set; }

        public string Password { get; set; }



        // Attrinutes to show result

        public string SessionName { get; set; }
        public string SemesterName { get; set; }
        public string CourseName { get; set; }
        public string PaperName { get; set; }
        public string ObtainedMarks { get; set; }
        public string TotalMarks { get; set; }



    }
}