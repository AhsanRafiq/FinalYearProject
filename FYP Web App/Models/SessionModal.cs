using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class SessionModal
    {


        public int Id { get; set; }
        public string SessionId { get; set; }

        public string SessionProgram { get; set; }

        //Stores the Start Day of Session
        public string SessionStartDay { get; set; }

        //Stores the Start Month of Session
        public string SessionStartMonth { get; set; }

        public string SessionStartYear { get; set; }

        //Stores the End Date of Session
        public string SessionEndDay { get; set; }

        public string SessionEndMonth { get; set; }

        public string SessionEndYear { get; set; }

    }
}