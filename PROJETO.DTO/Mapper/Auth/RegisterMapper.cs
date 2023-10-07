using PROJETO.Infra.Models;
using PROJETO.DTO.Request.Auth;

using BC = BCrypt.Net.BCrypt;
using Riok.Mapperly.Abstractions;

namespace PROJETO.DTO.Mapper.Auth;

[Mapper]
public partial class RegisterMapper 
{ 
    public UserModel RegisterRequestToModel(RegisterRequest request)
    {
        UserModel model = PrivateRegisterRequestToModel(request);
        model.Password = BC.HashPassword(model.Password);
        model.UpdatedAt = DateTime.UtcNow;
        model.CreatedAt = DateTime.UtcNow;
        return model;
    }
    
    private partial UserModel PrivateRegisterRequestToModel(RegisterRequest request);

    public LoginRequest RegisterRequestToLogin(RegisterRequest request)
    {
        return PrivateRegisterRequestToLogin(request);
    }
    
    private partial LoginRequest PrivateRegisterRequestToLogin(RegisterRequest request);
}
