using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cinema.WebUI.Models
{
    public class FilmControlModel
    {
        public Guid Id { get; set; }

        public bool HasAccess { get; set; }
    }
}
