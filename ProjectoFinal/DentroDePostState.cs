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
        private string circuloActual;
        private bool organizacion;
        public DentroDePostState(string nombrePost, string nombrePerfil, string nombreCirculo)
        {
            this.postActual = nombrePost;
            this.perfilActual = nombrePerfil;
            this.circuloActual = nombreCirculo;
        }
        public void Handle(StateMachine appState)
        {
            while (true)
            {
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.WriteLine("Has entrado al post: {0}", postActual);
                Console.ForegroundColor = ConsoleColor.Gray;
                //Imprime todos los comentarios existentes.
                ImprimirComentarioDeAutor(postActual);
                //Preguntale al usuario que desea hacer
                Console.WriteLine("¿Qué desea hacer? ");
                Console.WriteLine("1. Escribir un comentario dentro del post");
                Console.WriteLine("2. Eliminar un comentario dentro del post");
                Console.WriteLine("3. Subir el rating de un comentario");
                Console.WriteLine("4. Bajar el rating de un comentario");
                Console.WriteLine("5. Entrar a un comentario");
                Console.WriteLine("6. Volver");
                if (organizacion == true)
                {
                    Console.WriteLine("7. Ordenar por fecha");
                    Console.WriteLine("\n_________________________________________________\n");
                    SortPorRating(postActual);
                }
                else
                {
                    Console.WriteLine("7. Ordenar por rating");
                    Console.WriteLine("\n_________________________________________________\n");
                    SortPorFecha(postActual);
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
                                    comentario + "', " + rating + ", '" + today.ToString("yyyy-MM-dd") + "', '" + numeroDeComentario + "', '" + circuloActual + "')";
                                SQLManager.EjecutarQuery(query);

                                Console.Write("Su comentario ha sido añadido al post");
                                break;
                            }
                        case 2:
                            {
                                string numero = Console.ReadLine();
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
                            Console.Write("Escriba el numero del comentario al que desea ir: ");
                            byte num;
                            if (byte.TryParse(Console.ReadLine(), out num))
                            {
                                if (num <= ObtenNumeroComentarios(postActual))
                                {
                                    string nombreComentario = SQLManager.ObtenNombreComentario(postActual, num);
                                    appState.ChangeState(new DentroDeComentarioState(nombreComentario, postActual, perfilActual, circuloActual));

                                }
                            }
                            else
                                Console.WriteLine("Entrada no válida");
                            break;
                        case 6:
                            appState.GoBackToPrevious();    
                            break;
                        case 7:
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
                        default:
                            continue;
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
            DataTable table = SQLManager.ObtenTodosComentarios("comentarios", nombrePost);

            table.DefaultView.Sort = table.Columns[3].ColumnName + " DESC";
            table = table.DefaultView.ToTable();
            ImprimirTodosComentarios(table);
        }
        private void SortPorFecha(string nombrePost)
        {
            organizacion = false;
            DataTable table = SQLManager.ObtenTodosComentarios("comentarios", nombrePost);

            table.DefaultView.Sort = table.Columns[5].ColumnName + " DESC";
            table = table.DefaultView.ToTable();
            ImprimirTodosComentarios(table);
        }
        private void ImprimirTodosComentarios(DataTable table)
        {
            Console.WriteLine();
            Console.ForegroundColor = ConsoleColor.Green;
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine("Comentario {0}: {1} - {2} opina: {3}", row.ItemArray[5] , row.ItemArray[3], row.ItemArray[0], row.ItemArray[2]);

                ImprimirComentariosDeComentarios(row.ItemArray[2].ToString());
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        private void ImprimirComentariosDeComentarios(string comentario)
        {
            DataTable table = SQLManager.ObtenTodosComentarios("comentarios_comentarios", comentario);

            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine("             Comentario {0}: {1} - {2} opina: {3}", row.ItemArray[5], row.ItemArray[3], row.ItemArray[0], row.ItemArray[2]);
            }
        }

        public static int  ObtenNumeroComentarios(string nombrePost)
        {
            DataTable table = SQLManager.ObtenTodosComentarios("comentarios", nombrePost);
            return table.Rows.Count;
        }
    }
}
