using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using EasyCash.Application.Contracts;
using EasyCash.Domain;
using EasyCash.Domain.Entity;
using EasyCash.Infrastructure.Data;
using EasyCash.Infrastructure.Data.Repository;
using EasyCash.Infrastructure.Data.UnitOfWork;

namespace EasyCash.Infrastructure;


public static class Extensions
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        var connectionString = configuration["ConnectionStrings:DefaultConnection"];
        services.AddDbContext<EasyCashContext>(options =>
            options.UseSqlServer(connectionString, b =>
            {
                b.MigrationsAssembly(typeof(EasyCashContext).Assembly.FullName);
                b.EnableRetryOnFailure();
            })
        );

        services.AddTransient<IUserRepository, UserRepository>();
        services.AddTransient<IWalletRepository, WalletRepository>();
        services.AddTransient<ILoanRepaymentRepository, LoanRepaymentRepository>();
        services.AddTransient<ILoanRepository, LoanRepository>();
        services.AddTransient<IUnitOfWork, UnitOfWork>();

        return services;
    }
}