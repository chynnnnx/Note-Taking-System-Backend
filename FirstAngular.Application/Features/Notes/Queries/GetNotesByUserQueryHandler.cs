using AutoMapper;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.Notes.DTOs;
using FirstAngular.Application.Interfaces;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Notes.Queries
{
    public class GetNotesByUserQueryHandler : IRequestHandler<GetNotesByUserQuery, Result<PagedResult<NoteDTO>>>
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

        public async Task<Result<PagedResult<NoteDTO>>> Handle(GetNotesByUserQuery query, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                return Result<PagedResult<NoteDTO>>.Fail("User not logged in.");

         
            var (notes, totalCount) = await _unitOfWork.NoteRepository.GetNotesByUserAsync(
                userId,query.IsPinned,query.IsArchived, query.IsFavorite,  query.SearchTerm,query.PageNumber,
                query.PageSize,cancellationToken);


            var dtos = _mapper.Map<List<NoteDTO>>(notes);

            var pagedResult = new PagedResult<NoteDTO>
            {
                Data = dtos,
                TotalCount = totalCount,
                PageNumber = query.PageNumber,
                PageSize = query.PageSize
            };

            return Result<PagedResult<NoteDTO>>.Ok(pagedResult, "Notes loaded successfully.");
        }

    }
}
