using EasyCash.Application.Contracts;
using EasyCash.Application.Contracts.Handlers;
using EasyCash.Application.Exceptions;
using EasyCash.Application.Settings;
using EasyCash.Domain;
using EasyCash.Domain.Entity;
using EasyCash.Shared;
using EasyCash.Shared.Exceptions;

namespace EasyCash.Application.UseCases.Loan.AddLoan;

internal class AddLoanCommandHandler : ICommandHandler<AddLoanCommand, Result<string>>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly AppSettings _settings;
    public AddLoanCommandHandler(IUnitOfWork unitOfWork, AppSettings settings)
    {
        _unitOfWork = unitOfWork;
        _settings = settings;
    }

    public async Task<Result<string>> Handle(AddLoanCommand request, CancellationToken cancellationToken)
    {
        var userExists = await _unitOfWork.UsersRepository.FindOneAsync(x => x.Id == request.UserId && !x.IsDeleted);
        if (userExists is null) throw new NotFoundException("user not found");

        var existingLoan = await _unitOfWork.LoanRepository.FindAsync(x => x.UserId == request.UserId && !x.IsDeleted && x.Status != RepaymentStatus.Completed.ToString());
        if(existingLoan.FirstOrDefault() is not null) throw new DuplicateEntryException("user has an existing loan");
        
        //validate minimum loan amount
        if(request.LoanAmount < _settings.MinimumLoanAmount || request.LoanAmount > _settings.LoanLimit)
            throw new EasyCashException($"loan amount should be between {_settings.MinimumLoanAmount} and {_settings.LoanLimit}");
        //Based on assumption for just a month loan period
        var startDate = DateTime.UtcNow;
        var endDate = startDate.AddMonths(1);

        var loan = Loans.Create(userExists, request.LoanAmount, startDate, endDate, 1, 1, RepaymentStatus.Ongoing.ToString());

        await _unitOfWork.LoanRepository.AddAsync(loan);
        await _unitOfWork.CompleteAsync();

        //::TODO create loan repayment and update wallet.. . I can use INotificationHandler for optimization
        var loanRepayment = LoanRepayments.Create(loan, RepaymentStatus.Ongoing.ToString(), endDate, startDate);
        await _unitOfWork.LoanRepaymentRepository.AddAsync(loanRepayment);

        await FundUserWallet(userExists, CalculateDisbursableLoan(_settings.LoanPercentage, request.LoanAmount));

        await _unitOfWork.CompleteAsync();

        return AddLoanCommandResult.Success("created");
    }

    private decimal CalculateDisbursableLoan(double loanPercentage, decimal loanAmount)
    {
        var interest = (decimal)(loanPercentage/100) * loanAmount;
        return loanAmount - interest;
    }


    private async Task FundUserWallet(User user, decimal amount)
    {
        var userWallet  = await _unitOfWork.WalletRepository.FindOneAsync(x => x.UserId == user.Id);
        userWallet.FundWallet(amount);
        _unitOfWork.WalletRepository.Update(userWallet);
    }
}