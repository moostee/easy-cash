using EasyCash.Application.Commands;
using EasyCash.Domain;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Users.AddUser;


public record AddUserCommand(string Name, string Email, string Password, Role Role) : CommandBase<Result<string>>
{

}
