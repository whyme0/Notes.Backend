using AutoMapper;
using Notes.Application.Commands;
using Notes.Application.Common.Mappings;

namespace Notes.WebApi.Models
{
    public class UpdateNoteDTO : IMapWith<UpdateNoteCommand>
    {
        public Guid Id { get; set; }
        public string? Title { get; set; }
        public string? Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<UpdateNoteDTO, UpdateNoteCommand>();
        }
    }
}
