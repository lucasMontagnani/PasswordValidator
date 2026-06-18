using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PasswordValidator.Domain.Interfaces.Rules
{
    public interface IPasswordValidator
    {
        bool IsValid(string password);
    }
}
