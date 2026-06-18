using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.Domain.Interfaces.Rules
{
    public interface IPasswordRule
    {
        bool IsSatisfiedBy(string password);
    }
}
