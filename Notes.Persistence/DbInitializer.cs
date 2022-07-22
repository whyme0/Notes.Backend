namespace Notes.Persistence
{
    public class DbInitializer
    {
        public static void Initialize(NotesDbContext notesDbContext)
        {
            notesDbContext.Database.EnsureCreated();
        }
    }
}
