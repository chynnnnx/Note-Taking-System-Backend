using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandValidator: AbstractValidator<UpdateNoteCommand>
    {
        public UpdateNoteCommandValidator() {

            RuleFor(x => x.Title)
                 .MaximumLength(200).WithMessage("Title cannot exceed 300 characters.");
          

        }
    }
}
