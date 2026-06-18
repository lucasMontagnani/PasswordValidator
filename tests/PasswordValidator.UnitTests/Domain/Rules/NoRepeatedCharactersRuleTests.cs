using PasswordValidator.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.UnitTests.Domain.Rules
{
    public class NoRepeatedCharactersRuleTests
    {
        [Theory]
        [InlineData("", true)]
        [InlineData("abcdef", true)]
        [InlineData("aabcdef", false)]
        [InlineData("AbTp9!foo", false)]
        [InlineData("AbTp9!foA", false)]
        [InlineData("AbTp9!fok", true)]
        public void IsSatisfiedBy_ShouldDetectDuplicateCharacters(string password, bool expected)
        {
            var rule = new NoRepeatedCharactersRule();

            Assert.Equal(expected, rule.IsSatisfiedBy(password));
        }
    }
}
