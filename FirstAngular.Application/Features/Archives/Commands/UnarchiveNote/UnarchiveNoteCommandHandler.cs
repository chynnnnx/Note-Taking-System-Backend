using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Interfaces;
namespace FirstAngular.Application.Features.Archives.Commands.UnarchiveNote
{
    public class UnarchiveNoteCommandHandler: IRequestHandler<UnarchiveNoteCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public UnarchiveNoteCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task <Result<bool>> Handle (UnarchiveNoteCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId)) return Result<bool>.Fail("User not logged in.");

            var note = await _unitOfWork.NoteRepository.GetByIdAsync(command.Id);
            if (note == null || note.UserId != userId) return Result<bool>.Fail("Note not found");

            var success = note.Unarchive();
            if (!success) return Result<bool>.Fail("Note is already unarchived.");

            _unitOfWork.NoteRepository.Update(note);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }
    }
}
