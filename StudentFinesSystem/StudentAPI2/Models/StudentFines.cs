namespace StudentAPI2.Models
{
    public class StudentFines
    {
        public int Id { get; set; }

        public int FineId { get; set; }
        public decimal Fine { get; set; }

        public string FineName { get; set; }

        public string FineDescription { get; set; }

        public DateTime CreatedDate { get; set; }

        public string CreatedBy { get; set; }
    }
}
