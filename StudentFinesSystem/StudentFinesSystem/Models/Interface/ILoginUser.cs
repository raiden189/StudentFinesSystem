namespace StudentFinesSystem.Models.Interface
{
    public interface ILoginUser
    {
        string UserId { get; set; }
        bool IsAdmin { get; set; }
        string Token { get; set; }
        string Username { get; set; }
        string FirstName { get; set; }
        string LastName { get; set; }
        string Gender { get; set; }
        string MiddleName { get; set; }
        DateTime CreateDate { get; set; }
    }
}