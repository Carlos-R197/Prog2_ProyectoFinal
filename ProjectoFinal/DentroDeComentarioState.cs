﻿using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;
using System.Data;

namespace ProjectoFinal
{
    public class DentroDeComentarioState : IState
    {
        private string postActual;
        private string comentarioActual;
        private string perfilActual;
        private string circuloActual;

        public DentroDeComentarioState(string nombreComentario, string nombrePost, string nombrePerfil, string circuloActual)
        {
            this.comentarioActual = nombreComentario;
            this.postActual = nombrePost;
            this.perfilActual = nombrePerfil;
            this.circuloActual = circuloActual;
        }

        public void Handle(StateMachine appState)
        {
            Console.Write("Escriba su comentario: ");
            string comentario = Console.ReadLine();
            int rating = 0;
            DateTime today = DateTime.Today;
            int numeroDeComentario = ObtenNumeroComentarios(comentarioActual) + 1;

            string query = "INSERT INTO comentarios_comentarios VALUES('" + perfilActual + "', '" + comentarioActual + "', '" +
                comentario + "', " + rating + ", '" + today.ToString("yyyy-MM-dd") + "', '" + numeroDeComentario + "', '" +  circuloActual + "')";
            SQLManager.EjecutarQuery(query);
            Console.Write("Su comentario fue guardado");
        }

        private int ObtenNumeroComentarios(string nombrePost)
        {
            DataTable table = SQLManager.ObtenTodosComentarios("comentarios_comentarios", nombrePost);
            return table.Rows.Count;
        }
    }
}
