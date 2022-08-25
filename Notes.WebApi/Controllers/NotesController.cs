using Notes.Application.Queries;
using Notes.Application.Commands;
using Microsoft.AspNetCore.Mvc;
using Notes.WebApi.Models;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;

namespace Notes.WebApi.Controllers
{
    [ApiVersion("1.0")]
    //[ApiVersion("2.0")]
    [Produces("application/json")]
    [Route("api/{version:apiVersion}/[controller]")]
    [Authorize]
    public class NotesController : ControllerBase
    {
        private readonly IMapper _mapper;
        
        public NotesController(IMapper mapper)
        {
            _mapper = mapper;
        }
        
        /// <summary>
        /// Gets the list of current user's notes
        /// </summary>
        /// <remarks>
        /// Sample requrest: GET /note
        /// </remarks>
        /// <returns>
        /// Returns NoteList Data Transfer Object
        /// </returns>
        /// <response code="200">On success</response>
        /// <response code="401">When user is unauthorized</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
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

        /// <summary>
        /// Gets the note by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// GET /note/D34D349E-43B8-429E-BCA4-793C932FD580
        /// </remarks>
        /// <param name="id">Note id (guid)</param>
        /// <returns>Returns NoteDetailsVm</returns>
        /// <response code="200">Success</response>
        /// <response code="401">If the user in unauthorized</response>
        [HttpGet("{id}")]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
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

        /// <summary>
        /// Creates the note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// POST /note
        /// {
        ///     title: "note title",
        ///     details: "note details"
        /// }
        /// </remarks>
        /// <param name="createNoteDTO">CreateNoteDto object</param>
        /// <returns>Returns id (guid)</returns>
        /// <response code="201">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(401)]
        public async Task<ActionResult<Guid>> Create([FromBody] CreateNoteDTO createNoteDTO)
        {
            var command = _mapper.Map<CreateNoteCommand>(createNoteDTO);
            command.UserId = UserId;
            var noteId = await Mediator!.Send(command);
            return StatusCode(StatusCodes.Status201Created, noteId);
        }

        /// <summary>
        /// Updates the note
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// PUT /note
        /// {
        ///     title: "updated note title"
        /// }
        /// </remarks>
        /// <param name="updateNoteDTO">UpdateNoteDto object</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpPut]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
        public async Task<ActionResult> Update([FromBody] UpdateNoteDTO updateNoteDTO)
        {
            var command = _mapper.Map<UpdateNoteCommand>(updateNoteDTO);
            command.UserId = UserId;
            await Mediator!.Send(command);
            return NoContent();
        }

        /// <summary>
        /// Deletes the note by id
        /// </summary>
        /// <remarks>
        /// Sample request:
        /// DELETE /note/88DEB432-062F-43DE-8DCD-8B6EF79073D3
        /// </remarks>
        /// <param name="id">Id of the note (guid)</param>
        /// <returns>Returns NoContent</returns>
        /// <response code="204">Success</response>
        /// <response code="401">If the user is unauthorized</response>
        [HttpDelete("{id}")]
        [ProducesResponseType(204)]
        [ProducesResponseType(401)]
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
