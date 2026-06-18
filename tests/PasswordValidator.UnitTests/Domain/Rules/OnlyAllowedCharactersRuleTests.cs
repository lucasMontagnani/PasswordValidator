using PasswordValidator.Domain.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.UnitTests.Domain.Rules
{
    public class OnlyAllowedCharactersRuleTests
    {
        [Theory]
        [InlineData("AbTp9!fok", true)]
        [InlineData("AbTp9 fok", false)] // espaço não é permitido
        [InlineData("AbTp9#fok", true)]  // '#' está no conjunto especial
        [InlineData("", true)]           // vazia não viola essa regra, comprimento é responsabilidade de outra regra
        public void IsSatisfiedBy_ShouldRejectCharactersOutsideAllowedSet(string password, bool expected)
        {
            var rule = new OnlyAllowedCharactersRule();

            Assert.Equal(expected, rule.IsSatisfiedBy(password));
        }
    }
}
