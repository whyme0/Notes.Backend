using Notes.Application.Commands;
using Notes.Application.Common.Exceptions;
using Notes.Tests.Common;

namespace Notes.Tests.Notes.Commands
{
    public class DeleteNoteCommandHandlerTests : TestCommandBase
    {
        [Fact]
        public async Task DeleteNoteCommandHandler_Success()
        {
            var handler = new DeleteNoteCommandHandler(Context);

            await handler.Handle(
                new DeleteNoteCommand()
                {
                    Id = NotesContextFactory.NoteIdForDelete,
                    UserId = NotesContextFactory.UserAId
                }, CancellationToken.None);

            Assert.Null(Context.Notes!.SingleOrDefault(n => n.Id == NotesContextFactory.NoteIdForDelete));
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailureOnNonExistentId()
        {
            var handler = new DeleteNoteCommandHandler(Context);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await handler.Handle(
                    new DeleteNoteCommand()
                    {
                        Id = Guid.NewGuid(), 
                        UserId = NotesContextFactory.UserAId
                    }, CancellationToken.None);
            });
        }

        [Fact]
        public async Task DeleteNoteCommandHandler_FailureOnWrongUserId()
        {
            var deleteHandler = new DeleteNoteCommandHandler(Context);
            var createHandler = new CreateNoteCommandHandler(Context);
            var noteId = await createHandler.Handle(
                new CreateNoteCommand()
                {
                    UserId = NotesContextFactory.UserAId,
                    Title = "Title",
                    Details = "Details"
                }, CancellationToken.None);

            await Assert.ThrowsAsync<NotFoundException>(async () =>
            {
                await deleteHandler.Handle(
                    new DeleteNoteCommand()
                    {
                        UserId = NotesContextFactory.UserBId,
                        Id = noteId
                    }, CancellationToken.None);
            });
        }
    }
}
