using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class DentroDeComentarioState : IState
    {
        private string postActual;
        private string comentarioActual;
        private string perfilActual;

        public DentroDeComentarioState(string nombreComentario, string nombrePost, string nombrePerfil)
        {
            this.comentarioActual = nombreComentario;
            this.postActual = nombrePost;
            this.perfilActual = nombrePerfil;
        }

        public void Handle(StateMachine appState)
        {
            Console.Write("Escriba su comentario: ");
            string comentario = Console.ReadLine();
            int rating = 0;
            DateTime today = DateTime.Today;
            int numeroDeComentario = DentroDePostState.ObtenNumeroComentarios(comentarioActual) + 1;

            string query = "INSERT INTO comentarios VALUES('" + perfilActual + "', '" + postActual + "', '" +
                comentario + "', " + rating + ", '" + today.ToString("yyyy-MM-dd") + "', '" + numeroDeComentario + "', '" +  comentarioActual + "')";
            SQLManager.EjecutarQuery(query);
            Console.Write("Su comentario fue guardado");
        }
    }
}
