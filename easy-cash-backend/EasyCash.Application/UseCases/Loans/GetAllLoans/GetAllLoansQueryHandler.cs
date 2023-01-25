using EasyCash.Application.Contracts;
using EasyCash.Application.Contracts.Handlers;
using EasyCash.Domain.Entity;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.Loan.GetAllLoans;

internal class GetAllLoansQueryHandler : IQueryHandler<GetAllLoansQuery, Result<List<Loans>>>
{
    private readonly IUnitOfWork _untiOfWork;
    public GetAllLoansQueryHandler(IUnitOfWork untiOfWork)
    {
        _untiOfWork = untiOfWork;
    }
    public async Task<Result<List<Loans>>> Handle(GetAllLoansQuery request, CancellationToken cancellationToken)
    {
        var loans = await _untiOfWork.LoanRepository.GetAllLoansAndUserDetailsAsync();
        
        return GetAllLoansResult.Success(loans.ToList());
    }
}



