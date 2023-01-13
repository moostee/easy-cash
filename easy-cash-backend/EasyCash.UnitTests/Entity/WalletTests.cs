namespace EasyCash.UnitTests;

using NUnit.Framework;
using EasyCash.Domain;
using EasyCash.Domain.Entity;

public class WalletTests
{
    [SetUp]
    public void Setup()
    {

    }


    [Test]
    [TestCase("10000", "11000")]
    [TestCase("200000", "201000")]
    [TestCase("100.50", "1100.50")]
    public void FundWallet_WhenCalled_IncreasesAvailableBalance(string loanAmount, string expectedResult)
    {
        var user = User.Create("test", "test@gmail.com", "password", Role.USER, 0, true);
        var wallet = Wallets.Create(user, 1000);

        wallet.FundWallet(Convert.ToDecimal(loanAmount));

        Assert.That(wallet.AvailableBalance, Is.EqualTo(Convert.ToDecimal(expectedResult)));
    }

    [Test]
    [TestCase("10000", "0")]
    [TestCase("5000", "5000")]
    [TestCase("2000", "8000")]
    public void DebitWallet_WhenCalled_DecreasesAvailableBalance(string loanAmount, string expectedResult)
    {
        var user = User.Create("test", "test@gmail.com", "password", Role.USER, 0, true);
        var wallet = Wallets.Create(user, 10000);

        wallet.DebitWallet(Convert.ToDecimal(loanAmount));

        Assert.That(wallet.AvailableBalance, Is.EqualTo(Convert.ToDecimal(expectedResult)));
    }


    [Test]
    public void MakeLoanRepayment_WhenCalled_ConfirmZeroAvailableBalance()
    {
        var user = User.Create("test", "test@gmail.com", "password", Role.USER, 0, true);
        var wallet = Wallets.Create(user, 10000);

        wallet.MakeLoanRepayment();

        Assert.That(wallet.AvailableBalance, Is.EqualTo(0));
    }
}
