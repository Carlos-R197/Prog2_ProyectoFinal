﻿using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;
using System.Data;

namespace ProjectoFinal
{
    public class DentroDePostState : IState
    {
        private string postActual;
        private string perfilActual;
        public DentroDePostState(string nombrePost, string nombrePerfil)
        {
            this.postActual = nombrePost;
            this.perfilActual = nombrePerfil;
        }
        public void Handle(StateMachine appState)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Has entrado al post: {0}", postActual);
                //Imprime todos los comentarios existentes.
                ImprimirComentarioDeAutor(postActual);
                ImprimirTodosComentarios(postActual);
                //Preguntale al usuario que desea hacer
                Console.WriteLine("¿Qué desea hacer? ");
                Console.WriteLine("1. Escribir un comentario dentro del post");
                Console.WriteLine("2. Eliminar un comentario dentro del post");
                byte choice;

                if (byte.TryParse(Console.ReadLine(), out choice))
                {
                    switch (choice)
                    {
                        case 1:
                            {
                                Console.Write("Escriba su comentario: ");
                                string comentario = Console.ReadLine();
                                int rating = 0;
                                DateTime today = DateTime.Today;
                                int numeroDeComentario = ObtenNumeroComentarios(postActual) + 1;

                                string query = "INSERT INTO comentarios VALUES('" + perfilActual + "', '" + postActual + "', '" +
                                    comentario + "', " + rating + ", '" + today.ToString("yyyy-MM-dd") + "', '" + numeroDeComentario + "')";
                                SQLManager.EjecutarQuery(query);

                                Console.Write("Su comentario ha sido añadido al post");
                                break;
                            }
                        case 2:
                            {
                                Console.Write("Escriba el numero del comentario que desea borrar: ");
                                short opcion;
                                if (short.TryParse(Console.ReadLine(), out opcion))
                                {
                                    if (opcion <= ObtenNumeroComentarios(postActual))
                                    {
                                        string query = "DELETE FROM comentarios WHERE post_pertenece = " + "'" + postActual + "'" + " AND numero = " + opcion;
                                        SQLManager.EjecutarQuery(query);
                                    }
                                }
                                else
                                    Console.Write("Ese comentario no existe.");
                                break;
                            }
                    }
                }
            }
        }

        private void ImprimirComentarioDeAutor(string nombrePost)
        {
            DataTable table = SQLManager.ObtenComentarioAutor(nombrePost);
            Console.WriteLine("Autor: {0}", table.Rows[0].ItemArray[0]);
        }
        private void ImprimirTodosComentarios(string nombrePost)
        {
            DataTable table = SQLManager.ObtenTodosComentarios(nombrePost);

            Console.ForegroundColor = ConsoleColor.Green;
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine("Comentario {0}: {1} - {2} opina: {3}", row.ItemArray[5] , row.ItemArray[3], row.ItemArray[0], row.ItemArray[2]);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private int ObtenNumeroComentarios(string nombrePost)
        {
            DataTable table = SQLManager.ObtenTodosComentarios(nombrePost);
            return table.Rows.Count;
        }
    }
}
