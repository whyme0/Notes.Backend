using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Notes.Application.Queries
{
    public class NoteListDTO
    {
        public IList<NoteListElementDTO> Notes { get; set; }
    }
}
