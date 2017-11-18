using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class QuestionMCQ
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public string Text { get; set; }

        public int Correct { get; set; }

      
    }
}