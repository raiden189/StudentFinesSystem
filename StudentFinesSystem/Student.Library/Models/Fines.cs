using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Library.Models
{
    public class Fines
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int FineId { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }

    public class StudentFine
    {
        public string UserId { get; set; }
        public int FineId { get; set; }
        public string CreatedBy { get; set; }
    }

    public class DeleteStudentFine
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public int FineId { get; set; }
    }
}
