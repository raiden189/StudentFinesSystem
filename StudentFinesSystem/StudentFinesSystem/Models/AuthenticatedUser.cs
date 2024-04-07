using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.Models
{
    public class AuthenticatedUser
    {
        public string Access_Token { get; set; }

        public string UserId { get; set; }

        public string Username { get; set; }

        public bool IsAdmin { get; set; }
    }
}
