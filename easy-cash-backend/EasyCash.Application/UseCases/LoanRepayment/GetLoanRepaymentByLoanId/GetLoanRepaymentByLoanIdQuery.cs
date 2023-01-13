
using EasyCash.Application.Contracts;
using EasyCash.Application.Contracts.Handlers;
using EasyCash.Application.Exceptions;
using EasyCash.Application.Queries;
using EasyCash.Domain.Entity;
using EasyCash.Shared;

namespace EasyCash.Application.UseCases.LoanRepayment.GetLoanRepaymentById;


public record GetLoanRepaymentByLoanIdQuery(int LoanId) : QueryBase<Result<List<LoanRepayments>>>
{

}

public class GetLoanRepaymentByLoanIdQueryResult : Result<List<LoanRepayments>>
{ 

}


internal class GetLoanRepaymentByLoanIdQueryHandler : IQueryHandler<GetLoanRepaymentByLoanIdQuery, Result<List<LoanRepayments>>>
{
    private readonly IUnitOfWork _unitOfWork;
    public GetLoanRepaymentByLoanIdQueryHandler(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }
    public async Task<Result<List<LoanRepayments>>> Handle(GetLoanRepaymentByLoanIdQuery request, CancellationToken cancellationToken)
    {
        var existing = await _unitOfWork.LoanRepository.GetByIdAsync(request.LoanId);
        if(existing is null)
            throw new NotFoundException("loan not found");  

        var loanRepayments = await _unitOfWork.LoanRepaymentRepository.FindAsync(x => x.LoanId == request.LoanId); 
        return GetLoanRepaymentByLoanIdQueryResult.Success(loanRepayments.ToList());
    }

}

