using StudentFinesSystem.Models.Interface;

namespace StudentFinesSystem.Models
{
    public class LoginUser : ILoginUser
    {
        public bool IsAdmin { get; set; }
        public string Token { get; set; }
        public string Username { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public DateTime CreateDate { get; set; }
    }
}
