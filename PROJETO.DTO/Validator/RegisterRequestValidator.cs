using FluentValidation;

using PROJETO.DTO.Request.Auth;

namespace PROJETO.DTO.Validator;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(p => p.Name)
            .MinimumLength(5)
            .WithMessage("Nome muito curto, coloque pelo menos o sobrenome")
            .MaximumLength(40)
            .WithMessage("Nome muito grande");
    }
}
