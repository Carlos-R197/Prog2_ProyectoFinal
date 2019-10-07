using System;
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
        private bool organizacion;
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
                SortPorRating(postActual);
                //Preguntale al usuario que desea hacer
                Console.WriteLine("¿Qué desea hacer? ");
                Console.WriteLine("1. Escribir un comentario dentro del post");
                Console.WriteLine("2. Eliminar un comentario dentro del post");
                Console.WriteLine("3. Subir el rating de un comentario");
                Console.WriteLine("4. Bajar el rating de un comentario");
                if (organizacion == true)
                {
                    Console.WriteLine("5. Ordenar por fecha");
                }
                else
                {
                    Console.WriteLine("5. Ordenar por rating");
                }
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
                                string query = "DELETE FROM comentarios WHERE post_pertenece = " + "'" + postActual + "'" + " AND numero = ";
                                AfectarComentario(query);
                                break;
                            }
                        case 3:
                            {
                                Console.Write("Escriba el número del comentario al que desea subirle el rating: ");
                                string query = "UPDATE comentarios SET rating = rating + 1 WHERE post_pertenece = " +
                                            "'" + postActual + "'" + "AND numero = ";
                                AfectarComentario(query);
                                break;
                            }
                        case 4:
                            {
                                Console.Write("Escriba el número del comentario al que desea bajarle el rating: ");
                                string query = "UPDATE comentarios SET rating = rating - 1 WHERE post_pertenece = " +
                                            "'" + postActual + "'" + "AND numero = ";
                                AfectarComentario(query);
                                break;
                            }
                        case 5:
                            {
                                if (organizacion == true)
                                {
                                    SortPorFecha(postActual);
                                }
                                else
                                {
                                    SortPorRating(postActual);
                                }
                            }
                            break;
                    }
                }
            }
        }
        private void AfectarComentario(string query)
        {
            short opcion;
            if (short.TryParse(Console.ReadLine(), out opcion))
            {
                if (opcion <= ObtenNumeroComentarios(postActual))
                {
                    query += opcion;
                    SQLManager.EjecutarQuery(query);
                }
            }
            else
                Console.Write("Ese comentario no existe.");
        }

        private void ImprimirComentarioDeAutor(string nombrePost)
        {
            DataTable table = SQLManager.ObtenComentarioAutor(nombrePost);
            Console.WriteLine("Autor: {0}", table.Rows[0].ItemArray[0]);
        }
        private void SortPorRating(string nombrePost)
        {
            organizacion = true;
            DataTable table = SQLManager.ObtenTodosComentarios(nombrePost);

            table.DefaultView.Sort = table.Columns[4].ColumnName + " ASC";
            table = table.DefaultView.ToTable();
            ImprimirTodosComentarios(table);
        }
        private void SortPorFecha(string nombrePost)
        {
            organizacion = false;
            DataTable table = SQLManager.ObtenTodosComentarios(nombrePost);

            table.DefaultView.Sort = table.Columns[6].ColumnName + " ASC";
            table = table.DefaultView.ToTable();
            ImprimirTodosComentarios(table);
        }
        private void ImprimirTodosComentarios(DataTable table)
        {
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
