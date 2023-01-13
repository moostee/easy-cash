using EasyCash.Application.Contracts;
using EasyCash.Application.Contracts.Handlers;
using EasyCash.Domain;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Users.GetAllUsers;

internal class GetAllUsersQueryHandler : IQueryHandler<GetAllUsersQuery, Result<List<User>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetAllUsersQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<User>>> Handle(GetAllUsersQuery request, CancellationToken cancellationToken)
    {
        var users = await _unitOfWork.UsersRepository.GetAllAsync();
        return GetAllUsersResult.Success(users.ToList());
    }
}