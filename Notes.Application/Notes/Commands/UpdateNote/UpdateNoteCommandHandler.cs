using Notes.Domain;
using Notes.Application.Common.Exceptions;
using MediatR;
using Notes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Notes.Application.Commands
{
    public class UpdateNoteCommandHandler
        : IRequestHandler<UpdateNoteCommand>
    {
        private readonly INotesDbContext _dbContext;

        public UpdateNoteCommandHandler(INotesDbContext dbContext)
            => _dbContext = dbContext;

        public async Task<Unit> Handle(UpdateNoteCommand request, CancellationToken cancellationToken)
        {
            var note =
                await _dbContext.Notes.FirstOrDefaultAsync(note => note.Id == request.Id, cancellationToken);
            if (note == null || note.UserId != request.UserId)
            {
                throw new NotFoundException(nameof(Note), request.Id);
            }

            if (request.Title != null)
            {
                note.Title = request.Title;
            }
            if (request.Details != null)
            { 
                note.Details = request.Details;
            }
            note.EditDate = DateTime.Now;
            await _dbContext.SaveChangesAsync(cancellationToken);
            return Unit.Value;
        }
    }
}
