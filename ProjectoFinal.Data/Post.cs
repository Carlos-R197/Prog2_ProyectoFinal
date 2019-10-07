using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    public class Post : IComentario
    {
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public short RatingPost { get; set; }
        public string ComentarioInicialDelAutor { get; private set; }
        public DateTime FechaDePublicacion { get; private set; }
        public List<Comentario> Comentarios { get; set; }
        public int Edad { get; set; }

        public Post(string titulo, string autor, string comentarioInicialDelAutor, DateTime fechaDePublicacion, int edad)
        {
            this.Titulo = titulo;
            this.Autor = autor;
            this.RatingPost = 0;
            this.ComentarioInicialDelAutor = comentarioInicialDelAutor;
            this.FechaDePublicacion = fechaDePublicacion;
            Comentarios = new List<Comentario>();
            this.Edad = edad;
        }

        public void SubirRating()
        {
            RatingPost++;
            //Autor.SubirRating();
        }
        public void BajarRating()
        {
            RatingPost--;
            //Autor.BajarRating();
        }

        public void AddComentario(Comentario comentario)
        {
            Comentarios.Add(comentario);
        }
    }
}
