using EasyCash.Application.Contracts;
using EasyCash.Application.Contracts.Handlers;
using EasyCash.Application.Exceptions;
using EasyCash.Domain.Entity;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Wallet.GetWalletByUserId;

internal class GetWalletByUserIdQueryHandler : IQueryHandler<GetWalletByUserIdQuery, Result<Wallets>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetWalletByUserIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<Wallets>> Handle(GetWalletByUserIdQuery request, CancellationToken cancellationToken)
    {
        var existing = _unitOfWork.UsersRepository.GetByIdAsync(request.UserId);
        if(existing is null)
            throw new NotFoundException("user not found");  

        var wallet = await _unitOfWork.WalletRepository.FindAsync(x => x.UserId == request.UserId); 
        return GetWalletByUserIdResult.Success(wallet.FirstOrDefault());
    }

}
