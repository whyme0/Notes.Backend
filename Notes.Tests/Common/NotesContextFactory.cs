namespace Notes.Tests.Common
{
    public class NotesContextFactory
    {
        public static Guid UserAId = Guid.NewGuid();
        public static Guid UserBId = Guid.NewGuid();

        public static Guid NoteIdForDelete = Guid.NewGuid();
        public static Guid NoteIdForUpdate = Guid.NewGuid();

        public static NotesDbContext Create()
        {
            var options = new DbContextOptionsBuilder<NotesDbContext>()
                .UseInMemoryDatabase(Guid.NewGuid().ToString())
                .Options;

            var context = new NotesDbContext(options);

            context.Database.EnsureCreated();

            context.Notes!.AddRange(
                new Note()
                {
                    CreationDate = DateTime.Now,
                    EditDate = null,
                    Title = "Title1",
                    Details = "Details1",
                    Id = Guid.Parse("{62B1BFBB-51DC-4119-AA5C-FCC9A67DB8A8}"),
                    UserId = UserAId
                },
                new Note()
                {
                    CreationDate = DateTime.Now,
                    EditDate = null,
                    Title = "Title2",
                    Details = "Details2",
                    Id = Guid.Parse("{4BF26B16-1CEE-478B-9BAB-4DCF3559C941}"),
                    UserId = UserBId
                },
                new Note()
                {
                    CreationDate = DateTime.Now,
                    EditDate = null,
                    Title = "Title3",
                    Details = "Details3",
                    Id = NoteIdForDelete,
                    UserId = UserAId
                },
                new Note()
                {
                    CreationDate = DateTime.Now,
                    EditDate = null,
                    Title = "Title4",
                    Details = "Details4",
                    Id = NoteIdForUpdate,
                    UserId = UserBId
                }
            );

            context.SaveChanges();

            return context;
        }
        public static void Delete(NotesDbContext context)
        {
            context.Database.EnsureCreated();
            context.Dispose();
        }
    }
}
