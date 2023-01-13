using EasyCash.Application.Queries;
using EasyCash.Domain;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Users.GetAllUsers;

public record GetAllUsersQuery : QueryBase<Result<List<User>>>
{

}
