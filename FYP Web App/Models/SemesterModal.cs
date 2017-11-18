using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class SemesterModal
    {

        public int Id { get; set; }

        public int SessionId { get; set; }

        // Only for Getting the Sessions
        public string SessionName { get; set; }

        public string SemesterNo { get; set; }

        public string SemesterStartDay { get; set; }

        public string SemesterStartMonth { get; set; }

        public string SemesterStartYear { get; set; }

        public string SemesterEndDay { get; set; }

        public string SemesterEndMonth { get; set; }

        public string SemesterEndYear { get; set; }

        public string Active { get; set; }
    }
}