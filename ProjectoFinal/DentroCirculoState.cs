using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class DentroCirculoState : IState
    {
        private string currentPerfil;
        private string currentCirculo;
        private bool organizacion = true;

        public DentroCirculoState(string nombrePerfil, string nombreCirculoActual)
        {
            this.currentPerfil = nombrePerfil;
            this.currentCirculo = nombreCirculoActual;
        }

        public void Handle(StateMachine appState)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Has entrado al circulo: {0}", currentCirculo);

                //Hacer que se impriman todos los posts existentes. 
                SortPorRating();
                Console.WriteLine("¿Qué desea hacer?");
                Console.WriteLine("1. Crear un post");
                Console.WriteLine("2. Eliminar un post");
                Console.WriteLine("3. Ver un post existente");
                if (organizacion == true)
                {
                    Console.WriteLine("4. Ordenar por fecha");
                }
                else 
                {
                    Console.WriteLine("4. Ordenar por rating");
                }
                byte input = byte.Parse(Console.ReadLine());

                switch (input)
                {
                    case 1:
                        {
                            Console.Write("¿Cuál sera el título del post? ");
                            string tituloPost = Console.ReadLine();
                            Console.Write("¿Qué desea poner como su comentario? ");
                            string comentario = Console.ReadLine();
                            int rating = 0;
                            DateTime today = DateTime.Today;

                            string query = "INSERT INTO posts VALUES('" + tituloPost + "', '" + currentPerfil + "','" +
                                            currentCirculo + "', " + rating + ", '" + comentario + "', '" + today.ToString("yyyy-MM-dd") + "')";
                            SQLManager.EjecutarQuery(query);
                            Console.WriteLine("El post ha sido creado con exito");
                            Console.ReadLine();
                            break;
                        }
                    case 2:
                        {
                            Console.Write("¿Cómo se llama el post que desea eliminar? ");
                            string nombrePost = Console.ReadLine();
                            if (SQLManager.RevisaSiNombreExiste("posts", nombrePost))
                            {
                                string query = "DELETE FROM posts WHERE nombre = " + "'" + nombrePost + "'";
                                string query2 = "DELETE FROM comentarios WHERE post_pertenece = " + "'" + nombrePost + "'";
                                SQLManager.EjecutarQuery(query, query2);
                                Console.Write("El post fue borrado");
                            }
                            else
                            { Console.Write("Ese post no existe."); }
                            Console.ReadLine();
                            break;
                        }
                    case 3:
                        {
                            Console.Write("Escriba el nombre del post al que desea entrar: ");
                            string nombrePost = Console.ReadLine();
                            if (SQLManager.RevisaSiNombreExiste("posts", nombrePost))
                            {
                                appState.ChangeState(new DentroDePostState(nombrePost, currentPerfil));
                            }
                            else
                                Console.Write("Ese post no existe");
                        }
                        Console.ReadLine();
                        break;
                    case 4:
                        if (organizacion == true)
                        {
                            SortPorFecha();
                        }
                        else 
                        {
                            SortPorRating();
                        }
                        break;
                }
            }
        }
        private void SortPorRating() 
        {
            organizacion = true;
            DataTable table = SQLManager.ObtenTodosPost(currentCirculo);

            table.DefaultView.Sort = table.Columns[4].ColumnName + " ASC";
            table = table.DefaultView.ToTable();
            ImprimirNombreTodosPost(table);
        }
        private void SortPorFecha() 
        {
            organizacion = false;
            DataTable table = SQLManager.ObtenTodosPost(currentCirculo);

            table.DefaultView.Sort = table.Columns[6].ColumnName + " ASC";
            table = table.DefaultView.ToTable();
            ImprimirNombreTodosPost(table);
        }
        private void ImprimirNombreTodosPost(DataTable table)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine("Post: {0}, por {1} - {2}", row.ItemArray[0], row.ItemArray[1], row.ItemArray[3]);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
