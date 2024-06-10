using FluentValidation;
using MDE.Project.Rosseel_Almanzo.Domain.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace MDE.Project.Rosseel_Almanzo.Domain.Services.Validators
{
    public class CommentsValidator : AbstractValidator<Comment>
    {
        public CommentsValidator()
        {
            RuleFor(x => x.Content)
                .NotEmpty()
                .WithMessage("Content is required")
                .MinimumLength(2)
                .WithMessage("Content is to short")
                .MaximumLength(300)
                .WithMessage("Content is to long");
        }
    }
}
