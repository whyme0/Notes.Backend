using AutoMapper;
using Notes.Application.Commands;
using Notes.Application.Common.Mappings;

namespace Notes.WebApi.Models
{
    public class CreateNoteDTO : IMapWith<CreateNoteCommand>
    {
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNoteDTO, CreateNoteCommand>();
        }
    }
}
