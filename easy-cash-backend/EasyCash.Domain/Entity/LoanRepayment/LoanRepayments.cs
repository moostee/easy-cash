using System.ComponentModel.DataAnnotations.Schema;
using EasyCash.Shared.Entity;

namespace EasyCash.Domain.Entity;


public class LoanRepayments : BaseEntity<int>
{
    [ForeignKey("Loans")]
    public int? LoanId { get; private set; }
    public virtual Loans Loans { get; private set; }
    [System.ComponentModel.DataAnnotations.MaxLength(30)]
    public string Status { get; private set; }
    public DateTime StartDate { get; private set; }
    public DateTime EndDate { get; private set; }
    public DateTime DueDate { get; private set; }

    private LoanRepayments(Loans loans, string status, DateTime endDate, DateTime startDate)
    {
        LoanId = loans.Id;
        Status = status;
        DueDate = endDate;
        EndDate = endDate;
        StartDate = startDate;
    }
    public LoanRepayments()
    {

    }

    public void CompleteLoan() => Status = RepaymentStatus.Completed.ToString();

    public static LoanRepayments Create(Loans loans, string status, DateTime endDate, DateTime startDate)
        => new LoanRepayments(loans, status, endDate, startDate);

}


public enum RepaymentStatus
{
    Pending = 1,
    Ongoing,
    Completed
}
