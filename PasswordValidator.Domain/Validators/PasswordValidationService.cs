using PasswordValidator.Domain.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.Domain.Validators
{
    public class PasswordValidationService : IPasswordValidator
    {
        private readonly IReadOnlyCollection<IPasswordRule> _rules;

        public PasswordValidationService(IEnumerable<IPasswordRule> rules)
        {
            if (rules is null)
                throw new ArgumentNullException(nameof(rules));

            _rules = rules.ToList();

            if (_rules.Count == 0)
                throw new ArgumentException("At least one password rule must be provided.", nameof(rules));
        }

        public bool IsValid(string password)
        {
            return _rules.All(rule => rule.IsSatisfiedBy(password));
        }
    }
}
