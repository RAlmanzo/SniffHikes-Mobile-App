using FluentValidation;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Validators
{
    public class DogsValidator : AbstractValidator<Dog>
    {
        public DogsValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty()
                .WithMessage("Name is required")
                .MinimumLength(2)
                .WithMessage("Name is to short");

            RuleFor(x => x.DateOfBirth)
                .NotNull()
                .Must(BeValidDateOfBirth)
                .WithMessage("DateOfBirth cant be in the future");
        }

        private bool BeValidDateOfBirth(DateTime dateOfBirth)
        {
            return dateOfBirth <= DateTime.Now;
        }
    }
}
