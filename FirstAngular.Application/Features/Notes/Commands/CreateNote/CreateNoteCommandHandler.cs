using AutoMapper;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.DTOs;
using FirstAngular.Application.Interfaces;
using FirstAngular.Domain.Entities;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.Commands.CreateNote
{
    public class CreateNoteCommandHandler : IRequestHandler<CreateNoteCommand, Result<NoteDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public CreateNoteCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<NoteDTO>> Handle(CreateNoteCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                return Result<NoteDTO>.Fail("User not logged in.");

             var note = NoteEntity.Create(
                userId: userId,
                title: command.Title,
                content: command.Content,
                categoryId: command.CategoryId
            );

            await _unitOfWork.NoteRepository.AddAsync(note);
            await _unitOfWork.SaveChangesAsync();

             var dto = _mapper.Map<NoteDTO>(note);
            return Result<NoteDTO>.Ok(dto);
        }
    }
}
