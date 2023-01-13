using EasyCash.Application.Contracts;
using EasyCash.Domain;
using EasyCash.Domain.Entity;

namespace EasyCash.Infrastructure.Data.UnitOfWork;
public class UnitOfWork : IUnitOfWork
{
    private readonly EasyCashContext _context;
    public UnitOfWork(EasyCashContext context, ILoanRepository loanRepository, ILoanRepaymentRepository loanRepaymentRepository, IUserRepository usersRepository, IWalletRepository walletRepository)
    {
        _context = context;
        LoanRepository = loanRepository;
        LoanRepaymentRepository = loanRepaymentRepository;
        UsersRepository = usersRepository;
        WalletRepository = walletRepository;
    }

    public ILoanRepository LoanRepository { get; private set; }

    public ILoanRepaymentRepository LoanRepaymentRepository { get; private set; }

    public IUserRepository UsersRepository { get; private set; }

    public IWalletRepository WalletRepository { get; private set; }

    public int Complete() => _context.SaveChanges();

    public async Task<int> CompleteAsync() => await _context.SaveChangesAsync();
}
