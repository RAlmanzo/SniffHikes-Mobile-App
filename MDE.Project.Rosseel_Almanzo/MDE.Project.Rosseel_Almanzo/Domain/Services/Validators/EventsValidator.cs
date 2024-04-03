using FluentValidation;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Validators
{
    public class EventsValidator : AbstractValidator<Event>
    {
        public EventsValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty()
                .WithMessage("Title is required")
                .MinimumLength(2)
                .WithMessage("Title is to short");

            RuleFor(x => x.Description)
                .NotEmpty()
                .WithMessage("Description is required")
                .MinimumLength(2)
                .WithMessage("Description is to long")
                .MaximumLength(500)
                .WithMessage("Description is to long");

            RuleFor(x => x.Street)
                .NotEmpty()
                .WithMessage("Street is required");

            RuleFor(x => x.City)
                .NotEmpty()
                .WithMessage("City is required");

            RuleFor(x => x.Country)
                .NotEmpty()
                .WithMessage("Country is required");

            RuleFor(x => x.DateEvent)
                .NotNull()
                .Must(BeValidDate)
                .WithMessage("DateEvent cant be in de past");
        }

        private bool BeValidDate(DateTime dateOfBirth)
        {
            return dateOfBirth > DateTime.Now;
        }
    }
}
