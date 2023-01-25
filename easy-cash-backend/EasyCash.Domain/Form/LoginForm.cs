using FluentValidation;
using EasyCash.Shared.Exceptions;

namespace EasyCash.Domain.Entity;

public class LoginForm
{

    public string Email { get; set; }
    public string Password { get; set; }

    public void Validate()
    {
        var validator = new LoginFormValidator();
        var result = validator.Validate(this);
        if(!result.IsValid) throw new EasyCashException(result.Errors);
    }

}


public class LoginFormValidator : AbstractValidator<LoginForm>
{
    public LoginFormValidator()
    {
        RuleFor(x => x.Email).NotEmpty().EmailAddress().WithMessage("Please enter a valid email address");
        RuleFor(x => x.Password).NotEmpty().Matches("(?=[A-Za-z0-9@#$%^&+!=]+$)^(?=.*[a-z])(?=.*[A-Z])(?=.*[0-9])(?=.*[@#$%^&+!=])(?=.{8,}).*$").WithMessage("Minimum eight characters, at least one uppercase letter, one lowercase letter and one number");
    }
}


