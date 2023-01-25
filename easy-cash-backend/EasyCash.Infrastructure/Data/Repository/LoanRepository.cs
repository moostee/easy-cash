namespace EasyCash.Infrastructure.Data.Repository;
using EasyCash.Domain.Entity;
using Microsoft.EntityFrameworkCore;

internal class LoanRepository : Repository<Loans, int>, ILoanRepository
{
    public LoanRepository(EasyCashContext context) : base(context)
    {
    }

    public async Task<List<Loans>> GetAllLoansAndUserDetailsAsync()
    {
        return await _context.Loans.Include(x => x.User).ToListAsync().ConfigureAwait(false);
    }
}