using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.DTOs;
using FirstAngular.Application.Interfaces;
using AutoMapper;
namespace FirstAngular.Application.Features.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler: IRequestHandler<UpdateNoteCommand, Result<NoteDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public UpdateNoteCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task <Result<NoteDTO>> Handle (UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId)) return Result<NoteDTO>.Fail("User not logged in.");

            var note = await _unitOfWork.NoteRepository.GetByIdAsync(command.Id);
            if (note == null || note.UserId != userId)
                return Result<NoteDTO>.Fail("Note not found or access denied.");
            

            var updatedNote = _mapper.Map(command, note);
            updatedNote.UpdatedAt = DateTime.UtcNow;
            _unitOfWork.NoteRepository.Update(updatedNote);
            await _unitOfWork.SaveChangesAsync();
            var dto = _mapper.Map<NoteDTO>(updatedNote);
            return Result<NoteDTO>.Ok(dto);

        }
    }
}
