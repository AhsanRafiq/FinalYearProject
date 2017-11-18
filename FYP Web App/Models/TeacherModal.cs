using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FYP_Web_App.Models
{
    public class TeacherModal
    {

        public int Id { get; set; }

        public string EmployeeId { get; set; }

        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Designation { get; set; }

        public string Education { get; set; }

        public string ContactNumber { get; set; }


        public string PostalAddress { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }


        public byte[] Photo { get; set; }
    }
}