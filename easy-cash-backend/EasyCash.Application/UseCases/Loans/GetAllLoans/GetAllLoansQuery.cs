using EasyCash.Application.Queries;
using EasyCash.Domain.Entity;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Loan.GetAllLoans;

public record GetAllLoansQuery : QueryBase<Result<List<Loans>>>
{

}
