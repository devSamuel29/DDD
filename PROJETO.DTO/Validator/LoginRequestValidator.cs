using FluentValidation;

using PROJETO.DTO.Request.Auth;

namespace PROJETO.DTO.Validator;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(p => p.Email).EmailAddress();
    }
}
