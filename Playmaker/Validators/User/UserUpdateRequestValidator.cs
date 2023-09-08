using FluentValidation;
using Playmaker.Dtos;

namespace Playmaker.Validators;

public class UserUpdateRequestValidator : AbstractValidator<UserUpdateRequest>
{
    public UserUpdateRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100);
        RuleFor(x => x.Password).MaximumLength(100);
        RuleFor(x => x.Email).EmailAddress().MaximumLength(100);
    }
}