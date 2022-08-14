using Notes.Application.Commands;
using Notes.Tests.Common;

namespace Notes.Tests.Notes.Commands
{
    public class CreateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task CreateNoteCommandHandler_Success()
        {
            var handler = new CreateNoteCommandHandler(Context);
            string noteTitle = "Note title";
            string noteDetails = "Note details";

            var noteId = await handler.Handle(
                new CreateNoteCommand()
                {
                    Title = noteTitle,
                    Details = noteDetails,
                    UserId = NotesContextFactory.UserAId
                },
                CancellationToken.None);

            Assert.NotNull(await Context.Notes!.SingleOrDefaultAsync(n => n.Id == noteId));
        }
    }
}
