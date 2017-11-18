using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class PaperDetailsModal
    {
      
        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SemesterId { get; set; }
        public string[] Sections { get; set; }
        public int CourseId { get; set; }
        public string PaperName { get; set; }
        public int TeacherId { get; set; }
        public string NoOfQuestions { get; set; }
        public string TimeAllowed { get; set; }
        public string Active { get; set; }

        public string Password { get; set; }
        public int TotalMarks { get; set; }
        

        public List<PaperQuestionModal> listOfQuestions { get; set; }

        public string[] QuestionList { get; set; }
        //Only for getting the names of sections from DB
        public string SectionNames { get; set; }



        ////----Student Exam Related---///
        
        public string CourseName { get; set; }


    }
}