using EasyCash.Application.Commands;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Users.ActivateorDeactivateUser;


public record ActivateorDeactivateUserCommand(int UserId, string Action) : CommandBase<Result<string>>
{

}
