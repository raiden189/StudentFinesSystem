using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.Models
{
    public class FinesListModel
    {
        public int Id { get; set; }
        public decimal Fine { get; set; }
        public string FineName { get; set; }
        public string FineDescription { get; set; }
    }

    public class FinesList
    {
        public string Id { get; set; }
        public string Fine { get; set; }
        public string FineName { get; set; }
        public string FineDescription { get; set; }
    }
}
