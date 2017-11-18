using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class SectionModal
    {


        public int Id { get; set; }

        public int SessionId { get; set; }

        /// <summary>
        /// Only for holding the SessionName for list View
        /// </summary>
        public string SessionName { get; set; }


        public int SemesterId { get; set; }


        /// <summary>
        /// Only for holding the SemesterNo for list View
        /// </summary>
        public string SemesterNo { get; set; }

        public string SectionName { get; set; }

        public string Shift { get; set; }
    }
}