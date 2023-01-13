using EasyCash.Application.Contracts;
using EasyCash.Application.Contracts.Handlers;
using EasyCash.Application.Exceptions;
using EasyCash.Domain;
using EasyCash.Shared;
using EasyCash.Shared.Exceptions;

namespace EasyCash.Application.UseCases.Users.ActivateorDeactivateUser;

internal class ActivateorDeactivateUserCommandHandler : ICommandHandler<ActivateorDeactivateUserCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    public ActivateorDeactivateUserCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(ActivateorDeactivateUserCommand request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.UsersRepository.GetByIdAsync(request.UserId);
        if (existing is null)
            throw new NotFoundException("user not found");

        if(request.Action.ToLower() == EasyCash.Domain.Entity.Action.Activate.Value.ToLower())
            await ActivateUserAsync(existing);
        else
            await DeactivateUserAsync(existing);
        
        await _unitOfWork.CompleteAsync();

        return ActivateorDeactivateUserCommandResult.Success("success");
    }

    private async Task ActivateUserAsync(User existingUser)
    {
        if(!existingUser.IsDeleted) 
            throw new EasyCashException("user is already active");
        
        existingUser.ActivateUser();
        _unitOfWork.UsersRepository.Update(existingUser);
        
    }

    private async Task DeactivateUserAsync(User existingUser)
    {
        if(existingUser.IsDeleted) 
            throw new EasyCashException("user is already deactivated");
        
        existingUser.DeactivateUser();
        _unitOfWork.UsersRepository.Update(existingUser);

        
    }
}