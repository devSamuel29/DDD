using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;

namespace PROJETO.Domain.UseCases.Auth.Implementations;

public sealed class RegisterUseCase : IRegisterUseCase
{
    public IAuthRepository _repository;

    public RegisterUseCase(IAuthRepository repository)
    {
        _repository = repository;
    }

    public Task SignUp()
    {
        throw new NotImplementedException();
    }
}
