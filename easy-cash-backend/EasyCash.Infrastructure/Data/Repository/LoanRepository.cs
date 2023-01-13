namespace EasyCash.Infrastructure.Data.Repository;
using EasyCash.Domain.Entity;

internal class LoanRepository : Repository<Loans, int>, ILoanRepository
{
    public LoanRepository(EasyCashContext context) : base(context)
    {
    }
}