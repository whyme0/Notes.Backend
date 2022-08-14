using AutoMapper;
using Notes.Application.Queries;
using Notes.Tests.Common;

namespace Notes.Tests.Notes.Queries
{
    [Collection("QueryCollection")]
    public class GetNoteDetailsQueryHandlerTests
    {
        private readonly NotesDbContext _context;
        private readonly IMapper _mapper;

        public GetNoteDetailsQueryHandlerTests(QueryTestFixture fixture)
        {
            _context = fixture.Context;
            _mapper = fixture.Mapper;
        }

        [Fact]
        public async Task GetNoteDetailsQueryHandler_Success()
        {
            var handler = new GetNoteDetailsQueryHandler(_context, _mapper);

            var result = await handler.Handle(new GetNoteDetailsQuery()
            {
                UserId = NotesContextFactory.UserBId,
                Id = Guid.Parse("{4BF26B16-1CEE-478B-9BAB-4DCF3559C941}")
            }, CancellationToken.None);

            Assert.IsType<NoteDetailsDTO>(result);
            Assert.NotNull(_context.Notes!.SingleOrDefault(n => n.Id == result.Id));
        }
    }
}
