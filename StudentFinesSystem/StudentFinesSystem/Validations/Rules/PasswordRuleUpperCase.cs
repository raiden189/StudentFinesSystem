using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StudentFinesSystem.Validations.Rules
{
    public class PasswordRuleUpperCase<T> : IValidationRule<T>
    {
        public string ValidationMessage { get; set; }

        public bool Check(T value) =>
            value is string str && IsValidPassword(str);

        private bool IsValidPassword(string value) => value.Any(a => char.IsUpper(a));
    }
}
