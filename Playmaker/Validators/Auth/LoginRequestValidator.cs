using FluentValidation;
using Playmaker.Dtos;

namespace Playmaker.Validators;

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email).EmailAddress().MaximumLength(100).NotEmpty();
        RuleFor(x => x.Password).MaximumLength(100).NotEmpty();
    }
}