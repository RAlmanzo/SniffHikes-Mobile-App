using FluentValidation;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Validators
{
    public class UsersValidator : AbstractValidator<User>
    {
        public UsersValidator()
        {
            RuleFor(x => x.FirstName)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("FirstName is too short");

            RuleFor(x => x.LastName)
                .NotEmpty()
                .MinimumLength(2)
                .WithMessage("LastName is too short");

            RuleFor(x => x.Email)
                .NotEmpty()
                .WithMessage("Email is verplicht")
                .EmailAddress()
                .WithMessage("Email moet een geldig formaat hebben");

            RuleFor(x => x.Password)
                .NotEmpty()
                .MinimumLength(6)
                .WithMessage("Password is to short");

            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .Must(BeValidDateOfBirth)
                .WithMessage("DateOfBirth mag niet in de toekomst liggen");
        }

        private bool BeValidDateOfBirth(DateTime dateOfBirth)
        {
            return dateOfBirth <= DateTime.Now;
        }
    }
}
