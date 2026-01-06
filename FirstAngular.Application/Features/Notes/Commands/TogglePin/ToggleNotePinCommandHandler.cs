using FirstAngular.Application.Common.Results;
using MediatR;
using Microsoft.EntityFrameworkCore.ValueGeneration.Internal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.Interfaces;
using FirstAngular.Application.Features.Notes.DTOs;
namespace FirstAngular.Application.Features.Notes.Commands.TogglePin
{
    public class ToggleNotePinCommandHandler: IRequestHandler< ToggleNotePinCommand, Result<TogglePinDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public ToggleNotePinCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<TogglePinDTO>> Handle(ToggleNotePinCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId)) return Result<TogglePinDTO>.Fail("User not logged in.");
            var note = await _unitOfWork.NoteRepository.GetByIdAsync(command.Id);
            if (note == null || note.UserId != userId) return Result<TogglePinDTO>.Fail("Note not found");
            var changed = note.SetPinned(command.IsPinned);
            if (!changed) return Result<TogglePinDTO>.Fail("Pin state unchanged or note is archived");


            _unitOfWork.NoteRepository.Update(note);
            await _unitOfWork.SaveChangesAsync();

            return Result<TogglePinDTO>.Ok(new TogglePinDTO(note.Id, note.IsPinned));

        }
    }
}
