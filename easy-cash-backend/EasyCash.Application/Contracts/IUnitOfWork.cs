using EasyCash.Domain;
using EasyCash.Domain.Entity;

namespace EasyCash.Application.Contracts;


public interface IUnitOfWork
{
    ILoanRepository LoanRepository { get; }

    ILoanRepaymentRepository LoanRepaymentRepository { get; }
    IUserRepository UsersRepository { get; }
    IWalletRepository WalletRepository { get; }
    int Complete();

    Task<int> CompleteAsync();
}