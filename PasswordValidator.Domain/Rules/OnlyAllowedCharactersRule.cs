using PasswordValidator.Domain.Interfaces.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.Domain.Rules
{
    public class OnlyAllowedCharactersRule : IPasswordRule
    {
        public bool IsSatisfiedBy(string password)
        {
            return password.All(IsAllowedCharacter);
        }            

        private static bool IsAllowedCharacter(char character)
        {
            return char.IsDigit(character)
                || char.IsLower(character)
                || char.IsUpper(character)
                || SpecialCharacters.AllowedSet.Contains(character);
        }   
    }
}
