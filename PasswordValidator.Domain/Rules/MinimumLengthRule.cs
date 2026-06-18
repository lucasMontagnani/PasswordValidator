using PasswordValidator.Domain.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.Domain.Rules
{
    public class MinimumLengthRule : IPasswordRule
    {
        private readonly int _minimumLength;

        public MinimumLengthRule(int minimumLength = 9)
        {
            _minimumLength = minimumLength;
        }

        public bool IsSatisfiedBy(string password)
        {
            return password.Length >= _minimumLength;
        }
    }
}
