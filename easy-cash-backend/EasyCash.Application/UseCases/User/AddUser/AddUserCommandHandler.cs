using EasyCash.Application.Contracts;
using EasyCash.Application.Contracts.Handlers;
using EasyCash.Application.Exceptions;
using EasyCash.Domain;
using EasyCash.Domain.Entity;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Users.AddUser;

internal class AddUserCommandHandler : ICommandHandler<AddUserCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    public AddUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(AddUserCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.UsersRepository.FindOneAsync(x => x.Email == request.Email);

        if (existing is not null)
            throw new DuplicateEntryException("email already exists");

        var user = User.Create(request.Name, request.Email, BCrypt.Net.BCrypt.HashPassword(request.Password), request.Role);
        await _unitOfWork.UsersRepository.AddAsync(user);
        await _unitOfWork.CompleteAsync();

        //create wallet if role is user
        if (request.Role == Role.USER)
        {
            var wallet = Wallets.Create(user, 0);
            await _unitOfWork.WalletRepository.AddAsync(wallet);
            await _unitOfWork.CompleteAsync();
        }


        return AddUserCommandResult.Success("created");
    }
}
