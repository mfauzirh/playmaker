using FluentValidation;
using Playmaker.Dtos;

namespace Playmaker.Validators;

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Email).EmailAddress().MaximumLength(100).NotEmpty();
        RuleFor(x => x.Password).MaximumLength(100).NotEmpty();
        RuleFor(x => x.Role)
            .Must(role => role == "user" || role == "owner")
            .WithMessage(x => $"{x.Role} is not a valid role.")
            .MaximumLength(10)
            .NotEmpty();
    }
}