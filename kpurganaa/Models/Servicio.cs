using System;
using System.Collections.Generic;

namespace kpurganaa.Models
{
    public partial class Servicio
    {
        public Servicio()
        {
            PaquetesServicios = new HashSet<PaquetesServicio>();
        }

        public int IdServicio { get; set; }
        public string NombreServicio { get; set; } = null!;
        public string DescripcionServicio { get; set; } = null!;
        public decimal PrecioServicio { get; set; }
        public string EstadoServicio { get; set; } = null!;

        public virtual ICollection<PaquetesServicio> PaquetesServicios { get; set; }
    }
}
