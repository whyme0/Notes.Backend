using MediatR;
using Notes.Application.Interfaces;

namespace Notes.Application.Commands
{
    public class DeleteNoteCommand : IRequest
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
