using PasswordValidator.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.UnitTests.Domain.Rules
{
    public class ContainsDigitRuleTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("abcDEF", false)]
        [InlineData("abc1DEF", true)]
        public void IsSatisfiedBy_ShouldDetectAtLeastOneDigit(string password, bool expected)
        {
            var rule = new ContainsDigitRule();

            Assert.Equal(expected, rule.IsSatisfiedBy(password));
        }
    }
}
