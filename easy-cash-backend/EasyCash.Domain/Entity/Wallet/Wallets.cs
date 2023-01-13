using EasyCash.Shared.Entity;
using System.ComponentModel.DataAnnotations.Schema;

namespace EasyCash.Domain.Entity;


public class Wallets : BaseEntity<int>
{
    [ForeignKey("User")]
    public int? UserId { get; private set; }
    public virtual User User { get; private set; }

    public decimal AvailableBalance { get; private set; }


    private Wallets(User user, decimal availableBalance, int id = 0)
    {
        AvailableBalance = availableBalance;
        UserId = user.Id;
        Id = id;
    }

    public Wallets()
    {

    }

    public void FundWallet(decimal availableBalance)
    {
        AvailableBalance += availableBalance;
    }

    public void DebitWallet(decimal availableBalance)
    {
        AvailableBalance -= availableBalance;
    }

    public void MakeLoanRepayment()
    {
        AvailableBalance = 0;
    }

    public static Wallets Create(User user, decimal availableBalance, int id = 0)
     => new Wallets(user, availableBalance, id);

}
