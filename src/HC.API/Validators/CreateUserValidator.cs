using System;
using FluentValidation;
using HC.Application.DTOs;

namespace HC.API.Validators;

public class CreateUserValidator : AbstractValidator<UserCreateDto>
{
    public CreateUserValidator()
    {
        RuleFor(x => x.Username)
            .Length(5, 30)
            .WithMessage("Username must be longer than 5 and less than 30 symbols");

        RuleFor(x => x.Email)
            .EmailAddress()
            .WithMessage("Email must be in email format");

        RuleFor(x => x.BirthDate)
            .InclusiveBetween(DateTime.Now.AddYears(-100), DateTime.Now)
            .WithMessage("You must be less than 100 years old and not born later than now");

        RuleFor(x => x.Password)
            .Length(5, 50)
            .WithMessage("Password must be longer than 5");
    }
}