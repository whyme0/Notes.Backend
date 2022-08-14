using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Application.Interfaces;

namespace Notes.Tests.Common
{
    public class QueryTestFixture : IDisposable
    {
        public NotesDbContext Context { get; set; }
        public IMapper Mapper;
        
        public QueryTestFixture()
        {
            Context = NotesContextFactory.Create();
            var configurationBuilder = new MapperConfiguration(c =>
            {
                c.AddProfile(new AssemblyMappingProfile(typeof(INotesDbContext).Assembly));
            });
            Mapper = configurationBuilder.CreateMapper();
        }

        public void Dispose()
        {
            NotesContextFactory.Delete(Context);
        }

        [CollectionDefinition("QueryCollection")]
        public class QueryCollection : ICollectionFixture<QueryTestFixture> { }
    }
}
