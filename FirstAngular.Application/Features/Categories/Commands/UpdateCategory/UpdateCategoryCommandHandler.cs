using AutoMapper;
using FirstAngular.Application.Common.Helpers;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Features.Categories.DTOs;
using FirstAngular.Application.Interfaces;
using MediatR;
using Microsoft.IdentityModel.Tokens.Experimental;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FirstAngular.Application.Features.Categories.Commands.UpdateCategory
{
    public class UpdateCategoryCommandHandler: IRequestHandler<UpdateCategoryCommand, Result<CategoryDTO>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        private readonly ICurrentUserService _currentUserService;

        public UpdateCategoryCommandHandler(IUnitOfWork unitOfWork, IMapper mapper, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _currentUserService = currentUserService;
        }
       
        public async Task<Result<CategoryDTO>> Handle (UpdateCategoryCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;

            if (string.IsNullOrEmpty(userId)) return Result<CategoryDTO>.Fail("User not logged in.");
            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(command.Id);
            if (category == null || category.UserId != userId)
              return Result<CategoryDTO>.Fail("Category not found or access denied.");
            
            var hasChanges  = UpdateHelper.HasChanges((category.Name, command.Name));
            if (hasChanges ) category.Update(command.Name);

            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync( );

            var dto = _mapper.Map<CategoryDTO>(category);
            var message = UpdateHelper.GetMessage("Category", hasChanges);

            return Result<CategoryDTO>.Ok(dto, message);

        }
    }
}
