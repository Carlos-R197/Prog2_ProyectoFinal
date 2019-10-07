﻿using System;
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
                Console.WriteLine("¿Qué desea hacer?");
                Console.WriteLine("1. Crear un post");
                Console.WriteLine("2. Eliminar un post");
                Console.WriteLine("3. Ver un post existente");
                Console.WriteLine("4. Subir el rating de un post");
                Console.WriteLine("5. Bajar el rating de un post");
                Console.WriteLine("6. Volver");
                if (organizacion == true)
                {
                    Console.WriteLine("7. Ordenar por fecha");
                    SortPorRating();
                }
                else
                {
                    Console.WriteLine("7. Ordenar por rating");
                    SortPorFecha();
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
                            Console.Write("¿Cuál sera la edad minima para ver el post? ");
                            int edad;
                            if (int.TryParse(Console.ReadLine(), out edad))
                            {
                                if (edad > 150 || edad < 0)
                                {
                                    Console.Write("Numero demasiado grande o demasiado pequeño.");
                                    break;
                                }
                            }
                            else
                            {
                                Console.Write("Entrada no valida");
                                break;
                            }
                            int rating = 0;
                            DateTime today = DateTime.Today;
                            int numero = ObtenNumeroPost(currentCirculo) + 1;

                            string query = "INSERT INTO posts VALUES('" + tituloPost + "', '" + currentPerfil + "','" +
                                     currentCirculo + "', " + rating + ", '" + comentario + "', '" + today.ToString("yyyy-MM-dd") + "', " + numero + ", " + edad + ")";
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
                                if (SQLManager.RevisaSiPuedesBorrar(nombrePost, this.currentPerfil))
                                {
                                    string query = "DELETE FROM posts WHERE nombre = " + "'" + nombrePost + "'";
                                    string query2 = "DELETE FROM comentarios WHERE post_pertenece = " + "'" + nombrePost + "'";
                                    SQLManager.EjecutarQuery(query, query2);
                                    Console.Write("El post fue borrado");
                                }
                                else
                                {
                                    Console.WriteLine("No eres el creador de este post");
                                }
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
                                Perfil actualPerfil  = SQLManager.ObtenPerfil(currentPerfil);
                                Post actualPost = SQLManager.ObtenPost(nombrePost);
                                if (actualPerfil.Edad >= actualPost.Edad)
                                    appState.ChangeState(new DentroDePostState(nombrePost, currentPerfil));
                                else
                                    Console.Write("Usted no tiene la edad suficiente para entrar a este post");
                            }
                            else
                                Console.Write("Ese post no existe");
                        }
                        Console.ReadLine();
                        break;
                    case 4:
                        {
                            Console.Write("Escriba el número del post al que desea subirle el rating: ");
                            ManejaVotos(1);
                            break;
                        }
                    case 5:
                        {
                            Console.Write("Escriba el número del post al que desea bajarle el rating: ");
                            ManejaVotos(-1);
                            break;
                        }
                    case 6:
                        appState.ChangeState(MainMenuState.Instance);
                        break;
                    case 7:
                        {
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
                    default:
                        continue;
                }
            }
        }
        private void SortPorRating() 
        {
            organizacion = true;
            DataTable table = SQLManager.ObtenTodosPost(currentCirculo);

            table.DefaultView.Sort = table.Columns[3].ColumnName + " DESC";
            table = table.DefaultView.ToTable();
            ImprimirNombreTodosPost(table);
        }
        private void SortPorFecha() 
        {
            organizacion = false;
            DataTable table = SQLManager.ObtenTodosPost(currentCirculo);

            table.DefaultView.Sort = table.Columns[6].ColumnName + " DESC";
            table = table.DefaultView.ToTable();
            ImprimirNombreTodosPost(table);
        }
        private void ImprimirNombreTodosPost(DataTable table)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            foreach (DataRow row in table.Rows)
            {
                Console.WriteLine("{0}. Post: {1}, por {2} - {3}", row.ItemArray[6], row.ItemArray[0], row.ItemArray[1], row.ItemArray[3]);
            }
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        //private void ImprimirNombreTodosPost()
        //{
        //    DataTable table = SQLManager.ObtenTodosPost(currentCirculo);

        //    Console.ForegroundColor = ConsoleColor.Yellow;
        //    foreach (DataRow row in table.Rows)
        //    {
        //        Console.WriteLine("{0}. Post: {1}, por {2} - {3}", row.ItemArray[6], row.ItemArray[0], row.ItemArray[1], row.ItemArray[3]);
        //    }
        //    Console.ForegroundColor = ConsoleColor.Gray;
        //}
        private void ManejaVotos(int cantidad)
        {
            string query = "UPDATE posts SET rating = rating + " + cantidad + " WHERE circulo_pertenece = " +
                                        "'" + currentCirculo + "'" + "AND numero = ";

            short opcion;
            if (short.TryParse(Console.ReadLine(), out opcion))
            {
                if (opcion <= ObtenNumeroPost(currentCirculo))
                {
                    query += opcion;
                    SQLManager.EjecutarQuery(query);
                }
            }
            else
                Console.Write("Ese comentario no existe.");
        }
        private int ObtenNumeroPost(string nombreCirculo)
        {
            DataTable table = SQLManager.ObtenTodosPost(nombreCirculo);
            return table.Rows.Count;
        }

    }
}
