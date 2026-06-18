using PasswordValidator.Domain.Interfaces.Rules;
using PasswordValidator.Domain.Rules;
using PasswordValidator.Domain.Validators;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.UnitTests.Domain.Validators
{
    public class PasswordValidationServiceTests
    {
        private static IPasswordValidator CreateValidator()
        {
            var rules = new List<IPasswordRule>
            {
                new MinimumLengthRule(),
                new ContainsDigitRule(),
                new ContainsLowercaseLetterRule(),
                new ContainsUppercaseLetterRule(),
                new ContainsSpecialCharacterRule(),
                new OnlyAllowedCharactersRule(),
                new NoRepeatedCharactersRule()
            };

            return new PasswordValidationService(rules);
        }

        [Theory]
        [InlineData("", false)]
        [InlineData("aa", false)]
        [InlineData("ab", false)]
        [InlineData("AAAbbbCc", false)]
        [InlineData("AbTp9!foo", false)]
        [InlineData("AbTp9!foA", false)]
        [InlineData("AbTp9 fok", false)]
        [InlineData("AbTp9!fok", true)]
        public void IsValid_ShouldMatchSpecificationExamples(string password, bool expected)
        {
            var validator = CreateValidator();

            Assert.Equal(expected, validator.IsValid(password));
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenRulesIsNull()
        {
            Assert.Throws<ArgumentNullException>(() => new PasswordValidationService(null!));
        }

        [Fact]
        public void Constructor_ShouldThrow_WhenRulesIsEmpty()
        {
            Assert.Throws<ArgumentException>(() => new PasswordValidationService(new List<IPasswordRule>()));
        }
    }
}
