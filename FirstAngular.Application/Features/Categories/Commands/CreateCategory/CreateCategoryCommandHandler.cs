using FirstAngular.Application.Common.Results;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.DTOs;
using FirstAngular.Application.Interfaces;
using AutoMapper;
using FirstAngular.Domain.Entities;

namespace FirstAngular.Application.Features.Categories.Commands.CreateCategory
{
    public class CreateCategoryCommandHandler: IRequestHandler<CreateCategoryCommand, Result<CategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public CreateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }

        public async Task<Result<CategoryDTO>> Handle (CreateCategoryCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId)) return Result<CategoryDTO>.Fail("User not logged in");
            var category = _mapper.Map<CategoryEntity>(command);
            category.UserId = userId;
            category.CreatedAt = DateTime.UtcNow;

            await _unitOfWork.CategoryRepository.AddAsync(category);
            await _unitOfWork.SaveChangesAsync();

            var dto = _mapper.Map<CategoryDTO>(category);
            return Result<CategoryDTO>.Ok(dto);
        }
    }
}
