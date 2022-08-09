using Notes.Application.Queries;
using Notes.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Notes.WebApi.Models;
using AutoMapper;

namespace Notes.WebApi.Controllers
{
    [Route("api/[controller]")]
    public class NotesController : ControllerBase
    {
        private readonly IMapper _mapper;
        
        public NotesController(IMapper mapper)
        {
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<NoteListDTO>> GetAll()
        {
            Console.WriteLine("Before");
            var query = new GetNoteListQuery()
            {
                UserId = UserId
            }; 
            var notes = await Mediator!.Send(query);
            Console.WriteLine("After");
            return Ok(notes);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoteDetailsDTO>> Get(Guid id)
        {
            var query = new GetNoteDetailsQuery()
            {
                Id = id,
                UserId = UserId
            };
            var note = await Mediator!.Send(query);
            return Ok(note);
        }

        [HttpPost]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDTO createNoteDTO)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDTO);
            command.UserId = UserId;
            var noteId = await Mediator!.Send(command);
            return StatusCode(StatusCodes.Status201Created);
        }

        [HttpPut]
        public async Task<ActionResult> Update([FromBody] UpdateNoteDTO updateNoteDTO)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDTO);
            command.UserId = UserId;
            await Mediator!.Send(command);
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<ActionResult> Delete(Guid id)
        {
            var command = new DeleteNoteCommand()
            {
                Id = id,
                UserId = UserId
            };
            await Mediator!.Send(command);
            return NoContent();
        }
    }
}
