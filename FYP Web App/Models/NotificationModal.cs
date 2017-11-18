using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class NotificationModal
    {

        public int Id { get; set; }
        public int SessionId { get; set; }
        public int SemesterId { get; set; }
        public int CourseId { get; set; }
        public int TeacherId { get; set; }
        public string NotificationName { get; set; }
        public string NotificationDescription { get; set; }
        public string SessionName { get; internal set; }
        public string SemesterName { get; internal set; }
        public string CourseName { get; internal set; }
        public string TeacherName { get; internal set; }
    }
}