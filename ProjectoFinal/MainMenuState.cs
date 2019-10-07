using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class MainMenuState : IState
    {
        private static MainMenuState instance;

        public static MainMenuState Instance
        {
            get
            {
                if (instance == null)
                    instance = new MainMenuState();
                return instance;
            }
        }

        private Perfil currentPerfil;

        public void Handle(StateMachine appState)
        {
            Console.Clear();
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Nombre: {0}", currentPerfil.Nombre);
                Console.WriteLine("Rating General: {0} \n", currentPerfil.RatingGeneral);

                Console.WriteLine("¿Qué desea hacer?");
                Console.WriteLine("0. Cerrar sesión");
                Console.WriteLine("1. Revisar propio perfil");
                Console.WriteLine("2. Buscar un perfil existente");
                Console.WriteLine("3. Chat privado");                            
                Console.WriteLine("4. Ver Lista de circulos existentes");
                Console.WriteLine("5. Crear nuevo círculo.");
                Console.WriteLine("6. Borrar un circulo existente");
                Console.WriteLine("7. Suscribirse a circulo");
                Console.WriteLine("8. Salir de la aplicación");
                if (SQLManager.RevisaSiInvitado(this.currentPerfil)) 
                {
                    Console.ForegroundColor = ConsoleColor.Cyan;
                    Console.WriteLine("9. Revisar invitaciones");
                    Console.ResetColor();
                }
                Console.Write("R: ");
                byte input;

                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 0:
                            appState.GoBackToFirst();
                            break;
                        case 1:
                            currentPerfil.ImprimirInformacion();
                            Console.WriteLine("Presione (U) para visualizar su lista de amigos");
                            Console.WriteLine("Presione (Y) para modificar sus datos");
                            char desicion = Console.ReadLine()[0];
                            switch (char.ToLower(desicion))
                            {
                                case 'y':
                                    Console.Clear();
                                    string nombreViejo = currentPerfil.ModificarInfo();
                                    SQLManager.CambiarPerfil(this.currentPerfil, nombreViejo);
                                    Console.ReadLine();
                                    break;
                                case 'u':
                                    Console.Clear();
                                    SQLManager.ObtenerAmigos(this.currentPerfil);
                                    Console.ReadLine();
                                    break;
                                default:
                                    continue;
                            }
                            break;
                        case 2:
                            Console.Write("Escriba el nombre: ");
                            string nom = Console.ReadLine();
                            SQLManager.EncuentraPerfilesQueContienen(nom);
                            Console.ReadLine();
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("1. Crear un chat privado");
                            Console.WriteLine("2. Ver chats");
                            Console.WriteLine("3. Atras");
                            byte opcion;

                            if(byte.TryParse(Console.ReadLine(),out opcion))
                            {
                                switch (opcion)
                                {
                                    case 1:
                                        appState.ChangeState(new CreatingChatState(currentPerfil));
                                        break;
                                    case 2:
                                        SQLManager.VerChats(currentPerfil);
                                        Console.WriteLine("Escriba el nombre del Chat al que desea ingresar");
                                        string perfil = Console.ReadLine();
                                        Perfil perfil2 = new Perfil(perfil,"","",0,0);
                                        Chat chat = new Chat(currentPerfil,perfil2);
                                        appState.ChangeState(new ChattingState(chat));
                                        break;
                                    case 3:
                                        return;
                                }
                            }                           
                            break;
                        case 4:
                            {
                                SQLManager.ImprimirTodosCirculos();
                                Console.Write("Escriba el nombre del circulo al que desea entrar: ");
                                string nombreCirculo = Console.ReadLine();

                                if (SQLManager.RevisaSiNombreExiste("circulos", nombreCirculo))
                                {
                                    appState.ChangeState(new DentroCirculoState(currentPerfil.Nombre, nombreCirculo));
                                }
                                else
                                    Console.WriteLine("No existe ese circulo");
                                Console.ReadLine();
                            }
                            break;
                        case 5:
                            appState.ChangeState(new CreatingCirculoState(currentPerfil.Nombre));
                            break;
                        case 6:
                            {
                                Console.Write("Escriba el nombre del circulo que desea borrar: ");
                                string nombreCirculo = Console.ReadLine();

                                if (SQLManager.RevisaSiNombreExiste("circulos", nombreCirculo))
                                {
                                    if (SQLManager.RevisaSiPuedesBorrar(nombreCirculo, this.currentPerfil.Nombre))
                                    {
                                        string query = "DELETE FROM circulos WHERE nombre = " + "'" + nombreCirculo + "'";
                                        //SQLManager.BorrarCirculo(nombreCirculo);
                                        SQLManager.EjecutarQuery(query);
                                        Console.WriteLine("El círculo fue borrado.");
                                    }
                                    else
                                        Console.WriteLine("No eres el creador de este circulo");
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
                                    string query = "INSERT INTO circulos_perfiles VALUES('" + nombreCirculo + "', '"+ currentPerfil.Nombre +"' )";
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
                        case 9:
                            while (true)
                            {
                                Console.Clear();
                                Console.WriteLine("Escriba (aceptar) o (rechazar) dependiendo de lo que desee hacer con cada una de las invitaciones");
                                Console.WriteLine("Esriba (1) para salir");
                                SQLManager.VerInvitaciones("invitaciones", this.currentPerfil);
                                string s = SQLManager.AceptarRechazar(this.currentPerfil);

                                if (s == "1") 
                                {
                                    break;
                                }
                            }
                            break;
                        //Casos de prueba debajo, deben de ser implementados corretamente
                        case 10:
                            Console.Write("Escriba el nombre de la persona: ");
                            string nombre = Console.ReadLine();
                            SQLManager.EnviarInvitacion("perfiles_registrados", nombre, this.currentPerfil);
                            Console.ReadLine();
                            break;
                        default:
                            continue;
                    }
                }
            }
        }


        public void Inicializar(Perfil perfil)
        {
            if (instance == null)
            {
                this.currentPerfil = perfil;
                instance = this;
            }
        }
    }
}
