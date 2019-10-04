using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    public class Comentario : IComentario
    {
        public Perfil Autor { get; private set; }
        public string Mensaje { get; private set; }
        public DateTime FechaDePublicacion { get; private set; }
        public short RatingPost { get; set; }
        public List<Comentario> Comentarios { get; set; }

        public Comentario(Perfil autor, string mensaje, DateTime fechaDePublicacion)
        {
            this.Autor = autor;
            this.Mensaje = mensaje;
            this.FechaDePublicacion = fechaDePublicacion;
        }

        public void SubirRating()
        {
            RatingPost++;
            Autor.SubirRating();
        }
        public void BajarRating()
        {
            RatingPost--;
            Autor.BajarRating();
        }

        public void AddComentario(Comentario comentario)
        {
            Comentarios.Add(comentario);
        }
    }
}
