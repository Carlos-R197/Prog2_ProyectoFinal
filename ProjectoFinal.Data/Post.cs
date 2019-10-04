using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal.Data
{
    public class Post : IComentario
    {
        public string Titulo { get; private set; }
        public string Autor { get; private set; }
        public Comentario ComentarioInicialDelAutor { get; private set; }
        public DateTime FechaDePublicacion { get; private set; }
        public List<Comentario> Comentarios { get; set; }

        public Post(string titulo, string autor, Comentario comentarioInicialDelAutor, DateTime fechaDePublicacion)
        {
            this.Titulo = titulo;
            this.Autor = autor;
            this.ComentarioInicialDelAutor = comentarioInicialDelAutor;
            this.FechaDePublicacion = fechaDePublicacion;
            Comentarios = new List<Comentario>();
        }

        public void AddComentario(Comentario comentario)
        {
            Comentarios.Add(comentario);
        }
    }
}
