using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using EasyCash.Domain.Entity;

namespace EasyCash.Infrastructure.Data.Config;

public class LoanRepaymentConfig : IEntityTypeConfiguration<LoanRepayments>
{
    public void Configure(EntityTypeBuilder<LoanRepayments> builder)
    {
        builder.HasIndex(s => new { s.Id, s.LoanId })
            .IsUnique()
            .HasFilter(null);
    }
}