using AutoMapper;
using Notes.Application.Common.Mappings;
using Notes.Domain;

namespace Notes.Application.Queries
{
    public class NoteListElementDTO : IMapWith<Note>
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        
        public void Mapping(Profile profile)
        {
            profile.CreateMap<Note, NoteListElementDTO>();
        }
    }
}
