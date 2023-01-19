using EasyCash.Application.Contracts;
using EasyCash.Application.Contracts.Handlers;
using EasyCash.Application.Exceptions;
using EasyCash.Domain.Entity;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Loan.RepayLoan;

internal class RepayLoanCommandHandler : ICommandHandler<RepayLoanCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;

    public RepayLoanCommandHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<string>> Handle(RepayLoanCommand request, CancellationToken cancellationToken)
    {
        var exists = await _unitOfWork.UsersRepository.FindOneAsync(x => x.Id == request.UserId && !x.IsDeleted);
        if (exists is null) throw new NotFoundException("user not found");

        var loan = await _unitOfWork.LoanRepository.FindOneAsync(x => x.UserId == request.UserId && x.Status == RepaymentStatus.Ongoing.ToString());

        if(loan is null) return RepayLoanCommandResult.Failure("no active loan to repay");
        
        loan.CompleteLoan();
        _unitOfWork.LoanRepository.Update(loan);

        var loanRepayment = await _unitOfWork.LoanRepaymentRepository.FindOneAsync(x => x.LoanId == loan.Id && x.Status == RepaymentStatus.Ongoing.ToString());
        loanRepayment.CompleteLoan();
        _unitOfWork.LoanRepaymentRepository.Update(loanRepayment);

        var wallet = await _unitOfWork.WalletRepository.FindOneAsync(x => x.UserId == request.UserId);
        wallet.MakeLoanRepayment();
        _unitOfWork.WalletRepository.Update(wallet);


        await _unitOfWork.CompleteAsync();

        return RepayLoanCommandResult.Success("successful");
    }
}
