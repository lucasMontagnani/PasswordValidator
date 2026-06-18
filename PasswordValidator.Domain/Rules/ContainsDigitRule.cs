using PasswordValidator.Domain.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.Domain.Rules
{
    public class ContainsDigitRule : IPasswordRule
    {
        public bool IsSatisfiedBy(string password)
        {
            return password.Any(char.IsDigit);
        }
    }
}
