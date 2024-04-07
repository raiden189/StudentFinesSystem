using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.Models
{
    public class StudentModel
    {
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
    }
    
    public class Student
    {
        public string UserId { get; set; }
        public ImageSource StudentImage { get; set; }
        public string FullName { get; set; }
        public string Gender { get; set; }
    }
}
