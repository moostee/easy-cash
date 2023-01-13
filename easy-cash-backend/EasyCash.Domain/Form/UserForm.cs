using Ardalis.SmartEnum;
using FluentValidation;
using EasyCash.Shared;
using EasyCash.Shared.Exceptions;

namespace EasyCash.Domain.Entity;

public class UserForm
{
    public string Name { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    public Role Role { get; set; }

    public void Validate()
    {
        var validator = new UserFormValidator();
        var result = validator.Validate(this);
        if (!result.IsValid) throw new EasyCashException(result.Errors);
    }
}


public class UserFormValidator : AbstractValidator<UserForm>
{
    public UserFormValidator()
    {
        RuleFor(x => x.Email).NotEmpty().WithMessage("email is required");
        RuleFor(x => x.Password).NotEmpty().WithMessage("password is required");
        RuleFor(x => x.Name).NotEmpty().WithMessage("name is required");
        RuleFor(x => x.Role).NotEmpty().WithMessage("role is required");
    }
}

public class Action : SmartEnum<Action, string>
{
    public static readonly Action Activate = new(nameof(Activate), nameof(Activate));
    public static readonly Action Deactivate = new(nameof(Deactivate), nameof(Deactivate));

    protected Action(string name, string value) : base(name, value)
    {
    }
}


public class ActivateOrDeActivateUserForm
{
    public string Action { get; set; }
    public int UserId { get; set; }

    public void Validate()
    {
        Action.GetOrValidate<Action>();
        var validator = new ActivateOrDeActivateUserFormValidator();
        var result = validator.Validate(this);
        if (!result.IsValid) throw new EasyCashException(result.Errors);
    }

}

public class ActivateOrDeActivateUserFormValidator : AbstractValidator<ActivateOrDeActivateUserForm>
{
    public ActivateOrDeActivateUserFormValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("email is required");
    }
}



