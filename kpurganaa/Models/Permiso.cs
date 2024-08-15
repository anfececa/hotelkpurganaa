using System;
using System.Collections.Generic;

namespace kpurganaa.Models
{
    public partial class Permiso
    {
        public Permiso()
        {
            RolesPermisos = new HashSet<RolesPermiso>();
        }

        public int IdPermisos { get; set; }
        public string NombrePermiso { get; set; } = null!;
        public string? EstadoPermisos { get; set; }

        public virtual ICollection<RolesPermiso> RolesPermisos { get; set; }
    }
}
