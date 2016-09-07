using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace OCCEjecutivoAPI.Models
{
    public class Experience
    {
        public string Id { get; set; }
        public string JobName { get; set; }
        public string CompanyName { get; set; }
        public string JobDescription { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public string Address { get; set; }
    }
}