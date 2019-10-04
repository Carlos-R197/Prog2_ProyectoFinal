using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    public class Comentario
    {
        public string Autor { get; private set; }
        public string Mensaje { get; private set; }
        public DateTime FechaDePublicacion { get; private set; }

        public Comentario(string autor, string mensaje, DateTime fechaDePublicacion)
        {
            this.Autor = autor;
            this.Mensaje = mensaje;
            this.FechaDePublicacion = fechaDePublicacion;
        }
    }
}
