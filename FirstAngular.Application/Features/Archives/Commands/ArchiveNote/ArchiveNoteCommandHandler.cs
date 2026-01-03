using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Interfaces;
using MediatR;

namespace FirstAngular.Application.Features.Archives.Commands.ArchiveNote
{
    public class ArchiveNoteCommandHandler: IRequestHandler<ArchiveNoteCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        public ArchiveNoteCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<bool>> Handle (ArchiveNoteCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId)) return Result<bool>.Fail("User not logged in.");

            var note = await _unitOfWork.NoteRepository.GetByIdAsync(command.Id);
            if (note == null || note.UserId != userId) return Result<bool>.Fail("Note not found");

            var success = note.Archive();
            if (!success)
                return Result<bool>.Fail("Note is already archived.");

            _unitOfWork.NoteRepository.Update(note);
            await _unitOfWork.SaveChangesAsync();
            return Result<bool>.Ok(true);
        }
    }
}
