namespace EasyCash.Infrastructure.Data.Repository;
using EasyCash.Domain.Entity;

internal class WalletRepository : Repository<Wallets, int>, IWalletRepository
{
    public WalletRepository(EasyCashContext context) : base(context)
    {
    }
}
