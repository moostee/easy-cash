using EasyCash.Application.Queries;
using EasyCash.Domain.Entity;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Wallet.GetWalletByUserId;


public record GetWalletByUserIdQuery(int UserId) : QueryBase<Result<Wallets>>
{

}
