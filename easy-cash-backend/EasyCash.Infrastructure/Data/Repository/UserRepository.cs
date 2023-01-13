namespace EasyCash.Infrastructure.Data.Repository;
using EasyCash.Domain;

internal class UserRepository : Repository<User, int>, IUserRepository
{
    public UserRepository(EasyCashContext context) : base(context)
    {
    }
}
