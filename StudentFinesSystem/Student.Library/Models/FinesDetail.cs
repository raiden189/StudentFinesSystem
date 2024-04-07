using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Student.Library.Models
{
    public class FinesDetail
    {
        public int Id { get; set; }

        public decimal Fine { get; set; }

        public string FineName { get; set; }

        public string FineDescription { get; set; }

        public bool IsActive { get; set; }
    }
}
