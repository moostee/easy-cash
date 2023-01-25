namespace EasyCash.Domain.Entity;

public interface ILoanRepository : IRepository<Loans, int>
{
    Task<List<Loans>> GetAllLoansAndUserDetailsAsync();
}