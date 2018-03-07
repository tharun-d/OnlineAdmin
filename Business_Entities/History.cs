using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business_Entities
{
   public class History
    {
        public string UserName { get; set; }
        public DateTime DateOfExam { get; set; } 
        public double Percentage { get; set; }
        public string StatusOfExam { get; set; }
        public string SubjectName { get; set; }

    }
}
