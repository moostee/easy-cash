using EasyCash.Application.Queries;
using EasyCash.Domain.Entity;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Loan.GetLoanByUserId;


public record GetLoanByUserIdQuery(int UserId) : QueryBase<Result<List<Loans>>>
{

}
