using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class MainMenuState : IState
    {
        private Perfil currentPerfil;
        public MainMenuState(Perfil perfil)
        {
            this.currentPerfil = perfil;
        }

        public void Handle(StateMachine appState)
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Nombre: {0}", currentPerfil.Nombre);
                Console.WriteLine("Rating General: {0} \n", currentPerfil.RatingGeneral);

                Console.WriteLine("¿Qué desea hacer?");
                Console.WriteLine("1. Revisar propio perfil");
                Console.WriteLine("2. Buscar un perfil existente");
                Console.WriteLine("3. Crear un chat privado");
                Console.WriteLine("4. Ver Lista de circulos existentes");
                Console.WriteLine("5. Crear nuevo círculo.");
                Console.WriteLine("6. Borrar un circulo existente");
                Console.WriteLine("7. Suscribirse a circulo");
                Console.WriteLine("8. Salir de la aplicación");
                Console.Write("R: ");
                byte input;

                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            currentPerfil.ImprimirInformacion();
                            Console.WriteLine("Presione Y para modificar sus datos");
                            char desicion = Console.ReadLine()[0];
                            if (char.ToLower(desicion) == 'y')
                            {
                                Console.Clear();
                                currentPerfil.ModificarInfo();
                                SQLManager.CambiarPerfil(this.currentPerfil);
                                Console.ReadLine();
                            }
                            break;
                        case 2:
                            Console.Write("Escriba el nombre: ");
                            string nom = Console.ReadLine();
                            SQLManager.EncuentraPerfilesQueContienen(nom);
                            Console.ReadLine();
                            break;
                        case 3:
                            appState.ChangeState(new CreatingChatState(currentPerfil));
                            break;
                        case 4:
                            SQLManager.ImprimirTodosCirculos();
                            Console.ReadLine();
                            break;
                        case 5:
                            appState.ChangeState(new CreatingCirculoState());
                            break;
                        case 6:
                            {
                                Console.Write("Escriba el nombre del circulo que desea borrar: ");
                                string nombreCirculo = Console.ReadLine();

                                if (SQLManager.RevisaSiNombreExiste("circulos", nombreCirculo))
                                {
                                    string query = "DELETE FROM circulos WHERE nombre = " + "'" + nombreCirculo + "'";
                                    string query2 = "DROP TABLE circulo" + nombreCirculo;
                                    //SQLManager.BorrarCirculo(nombreCirculo);
                                    SQLManager.EjecutarQuery(query, query2);
                                    Console.WriteLine("El círculo fue borrado.");
                                }
                                else
                                    Console.WriteLine("Ese círculo no existe.");
                                Console.ReadLine();
                                break;
                            }
                        case 7:
                            {
                                Console.ForegroundColor = ConsoleColor.Cyan;
                                Console.WriteLine("Círculos existentes: ");
                                SQLManager.ImprimirTodosNombre("circulos");
                                Console.ForegroundColor = ConsoleColor.Gray;

                                Console.Write("Escriba el nombre del círculo al que desea suscribirse: ");
                                string nombreCirculo = Console.ReadLine();

                                if (SQLManager.RevisaSiNombreExiste("circulos", nombreCirculo))
                                {
                                    string query = "INSERT INTO circulo" + nombreCirculo + " VALUES('" + currentPerfil.Nombre + "')";
                                    //SQLManager.AñadirPerfilCirculo(nombreCirculo, currentPerfil.Nombre);
                                    SQLManager.EjecutarQuery(query);
                                    Console.WriteLine("Usted se ha suscrito al círculo {0}", nombreCirculo);
                                }
                                else
                                    Console.WriteLine("Ese círculo no existe");

                                Console.ReadLine();
                                break;
                            }
                        case 8:
                            Environment.Exit(0);
                            break;
                        default:
                            continue;
                    }
                }
            }
        }
    }
}
