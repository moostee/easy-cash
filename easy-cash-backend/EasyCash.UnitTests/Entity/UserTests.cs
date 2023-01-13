namespace EasyCash.UnitTests;

using NUnit.Framework;
using EasyCash.Domain;

public class UserTests
{
    [SetUp]
    public void Setup()
    {

    }


    [Test]
    public void ActivateUser_WhenCalled_ConfirmUserIsActivated()
    {
        var user = User.Create("test", "test@gmail.com", "password", Role.USER, 0, true);

        user.ActivateUser();

        Assert.That(user.IsDeleted, Is.EqualTo(false));
    }

    [Test]
    public void DeActivateUser_WhenCalled_ConfirmUserIsDeActivated()
    {
        var user = User.Create("test", "test@gmail.com", "password", Role.USER, 0, false);

        user.DeactivateUser();

        Assert.That(user.IsDeleted, Is.EqualTo(true));
    }
}
