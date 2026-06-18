using PasswordValidator.Domain.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.Domain.Rules
{
    public class NoRepeatedCharactersRule : IPasswordRule
    {
        public bool IsSatisfiedBy(string password)
        {
            string value = password ?? string.Empty;
            return value.Distinct().Count() == value.Length;
        }
    }
}
