using PasswordValidator.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.UnitTests.Domain.Rules
{
    public class MinimumLengthRuleTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("12345678", false)]
        [InlineData("123456789", true)]
        [InlineData("1234567890", true)]
        public void IsSatisfiedBy_ShouldRespectDefaultMinimumOfNine(string password, bool expected)
        {
            var rule = new MinimumLengthRule();

            Assert.Equal(expected, rule.IsSatisfiedBy(password));
        }

        [Fact]
        public void IsSatisfiedBy_ShouldRespectCustomMinimumLength()
        {
            var rule = new MinimumLengthRule(minimumLength: 3);

            Assert.True(rule.IsSatisfiedBy("abc"));
            Assert.False(rule.IsSatisfiedBy("ab"));
        }
    }
}
