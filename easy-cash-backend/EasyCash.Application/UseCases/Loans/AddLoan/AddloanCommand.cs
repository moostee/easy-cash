using EasyCash.Application.Commands;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Loan.AddLoan;


public record AddLoanCommand(decimal LoanAmount, int UserId, string StartDate, string EndDate) : CommandBase<Result<string>>
{
    
}
