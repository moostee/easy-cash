namespace EasyCash.Infrastructure.Data.Repository;
using EasyCash.Domain.Entity;

internal class LoanRepaymentRepository : Repository<LoanRepayments, int>, ILoanRepaymentRepository
{
    public LoanRepaymentRepository(EasyCashContext context) : base(context)
    {
    }
}
