using System;
using System.Collections.Generic;

namespace kpurganaa.Models
{
    public partial class Usuario
    {
        public Usuario()
        {
            Reservas = new HashSet<Reserva>();
        }

        public int IdUsuario { get; set; }
        public string CorreoUsuario { get; set; } = null!;
        public string ClaveUsuario { get; set; } = null!;
        public int? IdPersonas { get; set; }
        public int? IdRol { get; set; }

        public virtual Persona? IdPersonasNavigation { get; set; }
        public virtual Role? IdRolNavigation { get; set; }
        public virtual ICollection<Reserva> Reservas { get; set; }
    }
}
