using FirstAngular.Application.Features.Notes.Commands.CreateNote;
using FirstAngular.Application.Features.Notes.Commands.UpdateNote;
using FirstAngular.Application.Features.Notes.Commands.DeleteNote;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstAngular.Application.Features.Notes.Queries;
using FirstAngular.Application.Features.Notes.Commands.TogglePin;
using FirstAngular.Application.Features.Archives.Commands.ArchiveNote;
using FirstAngular.Application.Features.Archives.Commands.UnarchiveNote;
using Microsoft.AspNetCore.Authorization;
using FirstAngular.Application.Features.Favorites.Commands;

namespace FirstAngular.WebApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NoteController : ControllerBase
    {
        private readonly IMediator _mediator;
        public NoteController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpPost]
        public async Task<IActionResult> AddNote(CreateNoteCommand command)
        {
            var result = await _mediator.Send(command);
            if (!result.Success) return BadRequest(new { message = result.Error });

            return Ok(result.Data);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateNote(Guid id, UpdateNoteCommand command)
        {
            var commandWithId = command with { Id = id };

            var result = await _mediator.Send(commandWithId);
            if (!result.Success) return BadRequest(new { message = result.Error });
            return Ok(new { data = result.Data, message = result.Message });
        }
        [HttpPatch("{id}/toggle-pin")]
        public async Task<IActionResult> PinUnpinNote(Guid id, [FromBody] ToggleNotePinCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            if (!result.Success) return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }
        [HttpPatch("{id}/archive")]
        public async Task<IActionResult> ArchiveNote(Guid id, [FromBody] ArchiveNoteCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            if (!result.Success) return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }

        [HttpPatch("{id}/unarchive")]
        public async Task<IActionResult> UnArchiveNote (Guid id, [FromBody] UnarchiveNoteCommand command)
        {
            var commandWithId = command with { Id = id };
            var result = await _mediator.Send(commandWithId);
            if (!result.Success) return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }
        [HttpPatch("{id}/favorite")]
        public async Task<IActionResult> ToggleFavorite(Guid id)
        {
            var command = new ToggleFavoriteCommand(id);
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(new { message = result.Error });

            return Ok(result.Data);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var command = new DeleteNoteCommand (  id);
            var result = await _mediator.Send(command);

            if (!result.Success) return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }

        [HttpGet]
        public async Task<IActionResult> GetNotes([FromQuery] GetNotesByUserQuery query)
        {
            var result = await _mediator.Send(query);

            if (!result.Success)
                return BadRequest(new { message = result.Error });

            return Ok(new{data = result.Data.Data, total = result.Data.TotalCount,page = result.Data.PageNumber,pageSize = result.Data.PageSize,
                message = result.Message
            });
        }


    }
}
