using EasyCash.Application.Commands;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Loan.RepayLoan;


public record RepayLoanCommand(int UserId) : CommandBase<Result<string>>
{

}
