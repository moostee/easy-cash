using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using EasyCash.Shared.Entity;

namespace EasyCash.Domain.Entity;


public class Loans : BaseEntity<int>
{
    [ForeignKey("User")]
    public int UserId { get; private set; }
    public virtual User User { get; private set; }
    public decimal LoanAmount { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public int RepaymentCount { get; private set; }
    public int RepaymentCycle { get; private set; }
    [NotMapped]
    public bool IsCompleted => Status == RepaymentStatus.Completed.ToString();

    [MaxLength(30)]
    public string Status { get; private set; }
    public Loans() { }

    private Loans(User user, decimal loanAmount, DateTime startDate, DateTime endDate, int repaymentCount, int repaymentCycle, string status)
    {
        UserId = user.Id;
        LoanAmount = loanAmount;
        StartDate = startDate;
        EndDate = endDate;
        RepaymentCount = repaymentCount;
        RepaymentCycle = repaymentCycle;
        Status = status;
    }


    public void CompleteLoan()
    {
        Status = RepaymentStatus.Completed.ToString();
    }

    public static Loans Create(User user, decimal loanAmount, DateTime startDate, DateTime endDate, int repaymentCount, int repaymentCycle, string status)
     => new Loans(user, loanAmount, startDate, endDate, repaymentCount, repaymentCycle, status);

}
