using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class DentroDeComentarioState : IState
    {
        private string comentarioActual;

        public DentroDeComentarioState(string nombreComentario)
        {
            this.comentarioActual = nombreComentario;
        }

        public void Handle(StateMachine appState)
        {
            Console.Write("Escriba su comentario: ");
            string comentario = Console.ReadLine();
            int rating = 0;
            DateTime today = DateTime.Today;
            int numeroDeComentario = DentroDePostState.ObtenNumeroComentarios(comentarioActual) + 1;

            string query = "INSERT INTO comentarios VALUES('" + perfilActual + "', '" + postActual + "', '" +
                comentario + "', " + rating + ", '" + today.ToString("yyyy-MM-dd") + "', '" + numeroDeComentario + "')";
            SQLManager.EjecutarQuery(query);
        }
    }
}
