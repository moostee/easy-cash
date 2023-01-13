using MediatR;
using EasyCash.Application.Queries;

namespace EasyCash.Application.Contracts.Handlers
{
    public interface IQueryHandler<in TQuery, TResult> :
       IRequestHandler<TQuery, TResult>
       where TQuery : IQuery<TResult>
    {
    }
}
