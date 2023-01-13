using EasyCash.Application.Commands;
using EasyCash.Domain;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Auth.Login;


public record LoginCommand(string Email, string Password) : CommandBase<Result<AuthenticateResponseModel>>
{
    
}
