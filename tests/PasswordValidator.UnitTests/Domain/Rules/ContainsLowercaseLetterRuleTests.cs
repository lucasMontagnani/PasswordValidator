using PasswordValidator.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.UnitTests.Domain.Rules
{
    public class ContainsLowercaseLetterRuleTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("ABC123", false)]
        [InlineData("ABCabc123", true)]
        public void IsSatisfiedBy_ShouldDetectAtLeastOneLowercaseLetter(string password, bool expected)
        {
            var rule = new ContainsLowercaseLetterRule();

            Assert.Equal(expected, rule.IsSatisfiedBy(password));
        }
    }
}
