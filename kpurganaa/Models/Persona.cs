using System;
using System.Collections.Generic;

namespace kpurganaa.Models
{
    public partial class Persona
    {
        public Persona()
        {
            Usuarios = new HashSet<Usuario>();
        }

        public int IdPersonas { get; set; }
        public string Nombres { get; set; } = null!;
        public string Apellidos { get; set; } = null!;
        public string TipoDocumento { get; set; } = null!;
        public string NroDocumento { get; set; } = null!;
        public int Edad { get; set; }
        public string Celular { get; set; } = null!;
        public DateTime FechaNacimiento { get; set; }
        public int? IdRol { get; set; }

        public virtual ICollection<Usuario> Usuarios { get; set; }
    }
}
