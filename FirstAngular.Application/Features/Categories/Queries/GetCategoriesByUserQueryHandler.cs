using AutoMapper;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.Categories.DTOs;
using FirstAngular.Application.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Categories.Queries
{
    public class GetCategoriesByUserQueryHandler : IRequestHandler<GetCategoriesByUserQuery, Result<List<CategoryDTO>>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;
        private readonly IMapper _mapper;

        public GetCategoriesByUserQueryHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
            _mapper = mapper;
        }

        public async Task<Result<List<CategoryDTO>>> Handle(GetCategoriesByUserQuery query, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                return Result<List<CategoryDTO>>.Fail("User not logged in.");

            var categories = await _unitOfWork.CategoryRepository.GetCategoriesByUserAsync(
                userId, query.SearchTerm, query.PageNumber, query.PageSize, cancellationToken);

            var dtos = _mapper.Map<List<CategoryDTO>>(categories);
            return Result<List<CategoryDTO>>.Ok(dtos);
        }

    }
}
