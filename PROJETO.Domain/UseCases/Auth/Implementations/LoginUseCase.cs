using PROJETO.Domain.Repositories.Auth;
using PROJETO.Domain.UseCases.Auth.Abstractions;

namespace PROJETO.Domain.UseCases.Auth.Implementations;

public sealed class LoginUseCase : ILoginUseCase
{
    public IAuthRepository _repository;

    public LoginUseCase(IAuthRepository repository)
    {
        _repository = repository;
    }

    public Task SignIn()
    {
        throw new NotImplementedException();
    }
}
