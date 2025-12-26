using AutoMapper;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.DTOs;
using FirstAngular.Application.Interfaces;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.Queries
{
    public class GetNotesByUserQueryHandler : IRequestHandler<GetNotesByUserQuery, Result<List<NoteDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetNotesByUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<List<NoteDTO>>> Handle(GetNotesByUserQuery query, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                return Result<List<NoteDTO>>.Fail("User not logged in.");

             var notes = await _unitOfWork.NoteRepository.GetNotesByUserAsync(userId, query.IsPinned,
                 query.IsArchived, query.SearchTerm, query.PageNumber, query.PageSize, cancellationToken);

             var dtos = _mapper.Map<List<NoteDTO>>(notes);

            return Result<List<NoteDTO>>.Ok(dtos);
        }
    }
}
