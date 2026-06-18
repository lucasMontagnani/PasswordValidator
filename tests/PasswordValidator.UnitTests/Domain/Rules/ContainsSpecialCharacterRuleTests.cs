using PasswordValidator.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.UnitTests.Domain.Rules
{
    public class ContainsSpecialCharacterRuleTests
    {
        [Theory]
        [InlineData("", false)]
        [InlineData("abcABC123", false)]
        [InlineData("abc!ABC123", true)]
        [InlineData("abc+ABC123", true)]
        [InlineData("abc.ABC123", false)] // '.' não está no conjunto permitido
        public void IsSatisfiedBy_ShouldDetectAtLeastOneAllowedSpecialCharacter(string password, bool expected)
        {
            var rule = new ContainsSpecialCharacterRule();

            Assert.Equal(expected, rule.IsSatisfiedBy(password));
        }
    }
}
