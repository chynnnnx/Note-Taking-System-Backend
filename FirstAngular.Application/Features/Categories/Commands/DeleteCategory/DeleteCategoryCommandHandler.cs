using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Interfaces;


namespace FirstAngular.Application.Features.Categories.Commands.DeleteCategory
{
    public class DeleteCategoryCommandHandler: IRequestHandler<DeleteCategoryCommand, Result<object>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUserService;

        public DeleteCategoryCommandHandler(IUnitOfWork unitOfWork, ICurrentUserService currentUserService)
        {
            _unitOfWork = unitOfWork;
            _currentUserService = currentUserService;
        }

        public async Task<Result<object>> Handle(
        DeleteCategoryCommand command,
        CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (string.IsNullOrEmpty(userId))
                return Result<object>.Fail("User not logged in.");

            var category = await _unitOfWork.CategoryRepository.GetByIdAsync(command.Id);

            if (category == null || category.UserId != userId)
                return Result<object>.Fail("Category not found or access denied.");

           await  _unitOfWork.CategoryRepository.DeleteAsync(category); 
            await _unitOfWork.SaveChangesAsync();

            return Result<object>.Ok(new { message = "Category deleted successfully." });
        }

    }
}
    