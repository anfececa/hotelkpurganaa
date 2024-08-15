using System;
using System.Collections.Generic;

namespace kpurganaa.Models
{
    public partial class TiposHabitacione
    {
        public TiposHabitacione()
        {
            Habitaciones = new HashSet<Habitacione>();
        }

        public int IdTipoHabitacion { get; set; }
        public string Nombre { get; set; } = null!;
        public string Descripcion { get; set; } = null!;

        public virtual ICollection<Habitacione> Habitaciones { get; set; }
    }
}
