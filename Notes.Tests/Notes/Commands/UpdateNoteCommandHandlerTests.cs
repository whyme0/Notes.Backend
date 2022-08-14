using Notes.Application.Commands;
using Notes.Application.Common.Exceptions;
using Notes.Tests.Common;

namespace Notes.Tests.Notes.Commands
{
    public class UpdateNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task UpdateNoteCommandHandler_Success()
        {
            var handler = new UpdateNoteCommandHandler(Context);
            var newTitle = "New Title";

            await handler.Handle(new UpdateNoteCommand()
            {
                Id = NotesContextFactory.NoteIdForUpdate,
                UserId = NotesContextFactory.UserBId,
                Title = newTitle
            }, CancellationToken.None);

            Assert.NotNull(Context.Notes!.SingleOrDefault(n => n.Id == NotesContextFactory.NoteIdForUpdate && n.Title == newTitle));
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailureNonExistentNoteId()
        {
            var handler = new UpdateNoteCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(new UpdateNoteCommand()
                {
                    Id = Guid.NewGuid(),
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);
            });
        }

        [Fact]
        public async Task UpdateNoteCommandHandler_FailureOnWrongUserId()
        {
            var handler = new UpdateNoteCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(new UpdateNoteCommand()
                {
                    Id = NotesContextFactory.NoteIdForUpdate,
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);
            });
        }
    }
}
