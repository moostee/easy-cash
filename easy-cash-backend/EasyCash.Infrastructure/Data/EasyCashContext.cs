using System.Reflection;
using Microsoft.EntityFrameworkCore;
using EasyCash.Domain;
using EasyCash.Domain.Entity;
using EasyCash.Shared.Entity;

namespace EasyCash.Infrastructure.Data;

public class EasyCashContext : DbContext
{
    public EasyCashContext(DbContextOptions options) : base(options)
    {
        if (Database.IsRelational())
            Database.Migrate();
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var loanUser = User.Create(name: "Jane Doe", email: "janedoe@gmail.com", password: BCrypt.Net.BCrypt.HashPassword("P155w@rd"), role: Role.USER, id: 2);
        var adminUser = User.Create(name: "John Doe", email: "johndoe@gmail.com", password: BCrypt.Net.BCrypt.HashPassword("P1ssw@rd"), role: Role.ADMIN, id: 1);
        
        modelBuilder.Entity<User>().HasData
               (adminUser, loanUser);

        modelBuilder.Entity<Wallets>().HasData
               (EasyCash.Domain.Entity.Wallets.Create(loanUser, 0, 1));


        base.OnModelCreating(modelBuilder);
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }

    public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        AddModifiedOnTimeStamp();
        return base.SaveChangesAsync(cancellationToken);
    }

    private void AddModifiedOnTimeStamp()
    {
        foreach (var entry in ChangeTracker.Entries())
        {
            switch (entry.State)
            {
                case EntityState.Modified:
                    ((BaseEntityTimeStamp)entry.Entity).ModifiedOn = DateTime.UtcNow;
                    break;
            }
        }
    }

    public override int SaveChanges()
    {
        AddModifiedOnTimeStamp();
        return base.SaveChanges();
    }


    public DbSet<User> Users { get; set; }
    public DbSet<Loans> Loans { get; set; }
    public DbSet<LoanRepayments> LoanRepayments { get; set; }
    public DbSet<Wallets> Wallets { get; set; }

}