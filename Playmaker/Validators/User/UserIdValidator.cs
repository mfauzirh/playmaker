using FluentValidation;

namespace Playmaker.Validators;

public class UserIdValidator : AbstractValidator<int>
{
    public UserIdValidator()
    {
        RuleFor(userId => userId).GreaterThan(0).NotEmpty().WithName("userId");
    }
}