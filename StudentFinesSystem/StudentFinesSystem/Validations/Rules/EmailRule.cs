﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace StudentFinesSystem.Validations.Rules
{
    public class EmailRule<T> : IValidationRule<T>
    {
        //private readonly Regex _regex = new(@"^([w.-]+)@([w-]+)((.(w){2,3})+)$");
        private readonly Regex _regex = new(@"^([\w\.\-]+)@([\w\-]+)((\.(\w){2,3})+)$");

        public string ValidationMessage { get; set; }

        public bool Check(T value) =>
            value is string str && _regex.IsMatch(str);
    }
}
