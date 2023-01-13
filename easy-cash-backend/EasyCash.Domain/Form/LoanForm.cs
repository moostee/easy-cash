using FluentValidation;
using EasyCash.Shared.Exceptions;

namespace EasyCash.Domain.Entity;

public class LoanForm
{
    public decimal LoanAmount { get; set; }
    public string StartDate { get; set; }
    public string EndDate { get; set; }
    public int UserId { get; set; }


    public void Validate()
    {
        var validator = new LoanFormValidator();
        var result = validator.Validate(this);
        if (!result.IsValid) throw new EasyCashException(result.Errors);
    }

}

public class LoanFormValidator : AbstractValidator<LoanForm>
{
    public LoanFormValidator()
    {
        RuleFor(x => x.LoanAmount).NotEmpty().WithMessage("LoanAmount is required")
        .Custom((loanAmount, context) =>
        {
            if (loanAmount < 0) context.AddFailure("loan amount must be greater than 0");
        });
        RuleFor(x => x.StartDate).NotEmpty().WithMessage("StartDate is required");
        RuleFor(x => x.EndDate).NotEmpty().WithMessage("EndDate is required");
        RuleFor(x => x.UserId).NotEmpty().WithMessage("UserId is required");
    }
}


public class RepayLoanForm
{
    public int UserId { get; set; }
    public void Validate()
    {
        var validator = new RepayLoanFormValidator();
        var result = validator.Validate(this);
        if (!result.IsValid) throw new EasyCashException(result.Errors);
    }
}

public class RepayLoanFormValidator : AbstractValidator<RepayLoanForm>
{
    public RepayLoanFormValidator()
    {
        RuleFor(x => x.UserId).NotEmpty().WithMessage("userid is required");
    }
}
