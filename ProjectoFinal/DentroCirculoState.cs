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
                ImprimirNombreTodosPost();

                Console.WriteLine("¿Qué desea hacer?");
                Console.WriteLine("1. Crear un post");
                Console.WriteLine("2. Eliminar un post");
                Console.WriteLine("3. Ver un post existente");
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
                            break;
                        }
                    case 2:
                        {
                            Console.Write("¿Cómo se llama el post que desea eliminar? ");
                            string nombrePost = Console.ReadLine();
                            if (SQLManager.RevisaSiNombreExiste("posts", nombrePost))
                            {
                                string query = "DELETE FROM posts WHERE nombre = " + "'" + nombrePost + "'";
                                SQLManager.EjecutarQuery(query);
                                Console.Write("El post fue borrado");
                            }
                            else
                                Console.Write("Ese post no existe.");
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
                        break;
                }
                Console.ReadLine();
            }
        }

        private void ImprimirNombreTodosPost()
        {
            DataTable table = SQLManager.ObtenTodosPost(currentCirculo);

            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine("Post: {0}, por {1} - {2}", row.ItemArray[0], row.ItemArray[1], row.ItemArray[3]);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
    }
}
