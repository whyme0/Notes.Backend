using MediatR;

namespace Notes.Application.Queries
{
    public class GetNoteDetailsQuery : IRequest<NoteDetailsDTO>
    {
        public Guid UserId { get; set; }
        public Guid Id { get; set; }
    }
}
