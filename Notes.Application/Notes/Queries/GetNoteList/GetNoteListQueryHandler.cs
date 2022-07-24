using AutoMapper;
using MediatR;
using Notes.Application.Interfaces;
using Microsoft.EntityFrameworkCore;
using Notes.Domain;
using AutoMapper.QueryableExtensions;

namespace Notes.Application.Queries
{
    public class GetNoteListQueryHandler
        : IRequestHandler<GetNoteListQuery, NoteListDTO>
    {
        private readonly INotesDbContext _dbContext;
        private readonly IMapper _mapper;

        public GetNoteListQueryHandler(INotesDbContext dbContext, IMapper mapper)
        {
            _dbContext = dbContext;
            _mapper = mapper;
        }

        public async Task<NoteListDTO> Handle(GetNoteListQuery request, CancellationToken cancellationToken)
        {
            var notes = await _dbContext.Notes
                .Where(note => note.UserId == request.UserId)
                .ProjectTo<NoteListElementDTO>(_mapper.ConfigurationProvider)
                .ToListAsync(cancellationToken);

            return new NoteListDTO { Notes = notes };
        }
    }
}
