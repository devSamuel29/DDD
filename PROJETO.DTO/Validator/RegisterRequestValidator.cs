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

        RuleFor(p => p.Email).EmailAddress();

        RuleFor(p => p.Password)
            .Matches(@"/^(?=.*[$*&@#])$/")
            .WithMessage("A senha deve conter no mínimo um caractere especial!")
            .Matches(@"/^(?=.*[A-Z])$/")
            .WithMessage("A senha deve conter no mínimo uma letra maiúscula!")
            .Matches(@"/^r'\d.*\d'$/")
            .MinimumLength(8)
            .MaximumLength(20);
        
        RuleFor(p => p.BirthDay);
    }
}
