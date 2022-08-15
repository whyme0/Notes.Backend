using AutoMapper;
using Notes.Application.Commands;
using Notes.Application.Common.Mappings;
using System.ComponentModel.DataAnnotations;

namespace Notes.WebApi.Models
{
    public class CreateNoteDTO : IMapWith<CreateNoteCommand>
    {
        [Required]
        public string Title { get; set; }
        public string Details { get; set; }

        public void Mapping(Profile profile)
        {
            profile.CreateMap<CreateNoteDTO, CreateNoteCommand>();
        }
    }
}
