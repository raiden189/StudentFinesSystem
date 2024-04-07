using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.Models
{
    public class StudentFinesModel
    {
        public int Id { get; set; }

        public int FineId { get; set; }

        public decimal Fine { get; set; }

        public string FineName { get; set; }

        public string FineDescription { get; set; }

        public bool IsActive { get; set; }
    }

    public class StudentFines
    {
        public int Id { get; set; }

        public int FineId { get; set; }
        public string Fine { get; set; }

        public string FineName { get; set; }

        public string FineDescription { get; set; }
    }

    public class Fines
    {
        public string UserId { get; set; }

        public int FineId { get; set; }

        public string CreatedBy { get; set; }

        public DateTime CreatedDate { get; set; } = DateTime.Now;
    }

    public class StudentFineDelete
    {
        public int Id { get; set; }
        public string UserId { get; set; }

        public int FineId { get; set; }
    }
}
