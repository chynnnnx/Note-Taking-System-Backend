using MediatR;
using FirstAngular.Application.Common.Results;
using FirstAngular.Application.Interfaces;

namespace FirstAngular.Application.Features.Favorites.Commands
{
    public class ToggleFavoriteCommandHandler
        : IRequestHandler<ToggleFavoriteCommand, Result<bool>>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ICurrentUserService _currentUser;

        public ToggleFavoriteCommandHandler(
            IUnitOfWork unitOfWork,
            ICurrentUserService currentUser)
        {
            _unitOfWork = unitOfWork;
            _currentUser = currentUser;
        }

        public async Task<Result<bool>> Handle(ToggleFavoriteCommand command, CancellationToken cancellationToken)
        {
            var userId = _currentUser.UserId;
            if (string.IsNullOrWhiteSpace(userId)) return Result<bool>.Fail("User not logged in");

            var note = await _unitOfWork.NoteRepository.GetByIdAsync(command.Id);
            if (note == null) return Result<bool>.Fail("Note not found");

            if (!note.Favorite())  return Result<bool>.Fail("Cannot favorite archived note");

            _unitOfWork.NoteRepository.Update(note);
            await _unitOfWork.SaveChangesAsync();

            return Result<bool>.Ok(note.IsFavorite);
        }
    }
}
