using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Interfaces;

namespace FirstAngular.Application.Features.Notes.Commands.DeleteNote
{
    public class DeleteNoteCommandHandler: IRequestHandler<DeleteNoteCommand, Result<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        public DeleteNoteCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<object>> Handle(DeleteNoteCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId)) return Result<object>.Fail("User not logged in.");
            var note = await _unitOfWork.NoteRepository.GetByIdAsync(command.Id);

            if (note == null || note.UserId != userId)
                return Result<object>.Fail("Note not found or access denied.");
            
            _unitOfWork.NoteRepository.DeleteAsync(note);
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Ok(new { message = "Note deleted successfully." });
        }
    }
}
