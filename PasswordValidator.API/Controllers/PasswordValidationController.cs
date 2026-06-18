using Microsoft.AspNetCore.Mvc;
using PasswordValidator.API.Contracts;
using PasswordValidator.Domain.Interfaces.Rules;

namespace PasswordValidator.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public sealed class PasswordValidationController : ControllerBase
    {
        private readonly IPasswordValidator _passwordValidator;

        public PasswordValidationController(IPasswordValidator passwordValidator)
        {
            _passwordValidator = passwordValidator;
        }

        [HttpPost("password-validation")]
        [ProducesResponseType(typeof(ValidatePasswordResponse), StatusCodes.Status200OK)]
        public ActionResult<ValidatePasswordResponse> Validate([FromBody] ValidatePasswordRequest request)
        {
            var isValid = _passwordValidator.IsValid(request?.Password);

            return Ok(new ValidatePasswordResponse(isValid));
        }
    }
}
