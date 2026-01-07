using FirstAngular.Application.Common.Helpers;
using MediatR;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Interfaces;
using AutoMapper;
using FirstAngular.Application.Features.Notes.DTOs;

namespace FirstAngular.Application.Features.Notes.Commands.UpdateNote
{
    public class UpdateNoteCommandHandler : IRequestHandler<UpdateNoteCommand, Result<UpdateNoteDTO>>
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

        public async Task<Result<UpdateNoteDTO>> Handle(UpdateNoteCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                return Result<UpdateNoteDTO>.Fail("User not logged in.");

            var note = await _unitOfWork.NoteRepository.GetByIdAsync(command.Id);
            if (note == null || note.UserId != userId)
                return Result<UpdateNoteDTO>.Fail("Note not found or access denied.");

             var hasChanges = UpdateHelper.HasChanges((note.Title, command.Title), (note.Content, command.Content), (note.CategoryId, command.CategoryId));

             if (hasChanges)
                note.Update(command.Title, command.Content, command.CategoryId);

             _unitOfWork.NoteRepository.Update(note);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<UpdateNoteDTO>(note);

             var message = UpdateHelper.GetMessage("Note", hasChanges);

            return Result<UpdateNoteDTO>.Ok(dto, message);
        }
    }
}
