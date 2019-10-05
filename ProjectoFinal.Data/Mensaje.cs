using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    public class Mensaje
    {
        public DateTime HoraEnvio { get; set; }
        public Perfil perfil { get; set; }
        public string SMS { get; set; }

    }
}
