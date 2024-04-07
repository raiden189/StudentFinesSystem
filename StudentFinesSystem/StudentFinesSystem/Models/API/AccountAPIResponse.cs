using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.Models.API
{
    public class AccountAPIResponse<T>
    {
        public T Data { get; set; }

        public List<Error> ErrorList { get; set; } = new List<Error>();
    }

    public class Error
    {
        public string Code { get; set; }
        public string Description { get; set; }
    }
}
