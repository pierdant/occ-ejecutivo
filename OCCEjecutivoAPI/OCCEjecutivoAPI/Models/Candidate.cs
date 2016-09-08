using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCCEjecutivoAPI.Models
{
    public class Candidate
    {
        public string id { get; set; }
        public string FirstName { get; set; }
        public string LastName_1 { get; set; }
        public string LastName_2 { get; set; }
        public string NickName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Title { get; set; }
    }
}