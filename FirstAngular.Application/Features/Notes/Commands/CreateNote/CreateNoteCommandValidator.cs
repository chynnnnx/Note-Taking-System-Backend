using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.Commands.CreateNote
{
    public class CreateNoteCommandValidator: AbstractValidator<CreateNoteCommand>   
    {
        public CreateNoteCommandValidator()
        {
            RuleFor(x => x.Title)
             .NotEmpty().WithMessage("Title is required.")
             .MaximumLength(300).WithMessage("Title cannot exceed 100 characters.");
            RuleFor(x => x.Content)
                .NotEmpty().WithMessage("Content is required.");
        }
    }
}
