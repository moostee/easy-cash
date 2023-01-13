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
        RuleFor(x => x.Email).NotEmpty().WithMessage("email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("password is required");
    }
}


