namespace EasyCash.UnitTests;

using NUnit.Framework;
using EasyCash.Domain;
using EasyCash.Domain.Entity;

public class LoanTests
{
    [SetUp]
    public void Setup()
    {

    }


    [Test]
    public void CompleteLoan_WhenCalled_ConfirmLoanIsCompleted()
    {
        var user = User.Create("test", "test@gmail.com", "password", Role.USER, 0, true);
        var loan = Loans.Create(user, 1000, DateTime.UtcNow, DateTime.UtcNow, 1, 1, RepaymentStatus.Ongoing.ToString());

        loan.CompleteLoan();

        Assert.That(loan.Status, Is.EqualTo(RepaymentStatus.Completed.ToString()));
    }
}
