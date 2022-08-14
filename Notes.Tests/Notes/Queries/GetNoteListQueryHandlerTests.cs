using AutoMapper;
using Notes.Application.Queries;
using Notes.Tests.Common;

namespace Notes.Tests.Notes.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteListQueryHandlerTests
    {
        private readonly NotesDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteListQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async void GetNoteListQueryHandler_Success()
        {
            var handler = new GetNoteListQueryHandler(_context, _mapper);

            var result = await handler.Handle(
                new GetNoteListQuery()
                {
                    UserId = NotesContextFactory.UserBId
                }, CancellationToken.None);

            Assert.IsType<NoteListDTO>(result);
            Assert.Equal(2, _context.Notes!.Where(x => x.UserId == NotesContextFactory.UserBId).Count());
        }
    }
}
