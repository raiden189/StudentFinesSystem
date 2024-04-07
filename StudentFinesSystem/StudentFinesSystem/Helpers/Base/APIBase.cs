using Microsoft.Extensions.Configuration;
using StudentFinesSystem.Models;
using StudentFinesSystem.Models.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.Helpers.Base
{
    public class APIBase
    {
        public APICaller _api;

        public ILoginUser _loginUser;

        public APIBase(IConfiguration configuration, ILoginUser loginUser)
        {
            _api = new APICaller(configuration);
            _loginUser = loginUser;
        }
    }
}
