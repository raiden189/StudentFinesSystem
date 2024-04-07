namespace StudentAPI.Models
{
    public class LoginModel
    {
        public string Username { get; set; } = string.Empty;

        public string Password { get; set; } = string.Empty;

        public override int GetHashCode()
        {
            return (Username.GetHashCode() ^ Password.GetHashCode());
        }
    }
}
