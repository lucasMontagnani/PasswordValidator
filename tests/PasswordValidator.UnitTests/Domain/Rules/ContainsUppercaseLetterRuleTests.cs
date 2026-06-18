using PasswordValidator.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.UnitTests.Domain.Rules
{
    public class ContainsUppercaseLetterRuleTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("abc123", false)]
        [InlineData("abcABC123", true)]
        public void IsSatisfiedBy_ShouldDetectAtLeastOneUppercaseLetter(string password, bool expected)
        {
            var rule = new ContainsUppercaseLetterRule();

            Assert.Equal(expected, rule.IsSatisfiedBy(password));
        }
    }
}
