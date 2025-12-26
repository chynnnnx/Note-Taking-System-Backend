using FirstAngular.Application.Features.Notes.Commands.CreateNote;
using FirstAngular.Application.Features.Notes.Commands.UpdateNote;
using FirstAngular.Application.Features.Notes.Commands.DeleteNote;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using FirstAngular.Application.Features.Notes.Queries;
using FirstAngular.Application.Features.Notes.Commands.TogglePin;

namespace FirstAngular.WebApi.Controllers
{
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
            command.Id = id;

            var result = await _mediator.Send(command);
            if (!result.Success) return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }
        [HttpPatch("{id}/pin")]
        public async Task<IActionResult> PinUnpinNote(Guid id)
        {
            var command = new ToggleNotePinCommand { Id = id, IsPinned = true };
            var result = await _mediator.Send(command);
            if (!result.Success) return BadRequest(new { message = result.Error });
            return Ok(result.Data);
        }



        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNote(Guid id)
        {
            var command = new DeleteNoteCommand { Id = id };
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

            return Ok(result.Data);
        }


    }
}
