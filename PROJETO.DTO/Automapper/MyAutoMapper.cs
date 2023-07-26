
using PROJETO.DTO.Request.Auth;
using PROJETO.Infra.Models;

using AutoMapper;

namespace PROJETO.DTO.Automapper;

public class MyAutoMapper : Profile
{
    public MyAutoMapper() 
    { 
        CreateMap<RegisterRequest, UserModel>();
        CreateMap<RegisterRequest, LoginRequest>();
    }
}
