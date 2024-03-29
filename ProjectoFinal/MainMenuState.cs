﻿using System;
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
                            instance = null;
                            appState.GoBackToFirst();
                            break;
                        case 1:
                            Console.Clear();
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
                            Console.Clear();
                            Console.Write("Escriba el nombre de la persona a agregar: ");
                            string nom = Console.ReadLine();
                            SQLManager.EncuentraPerfilesQueContienen(nom);
                            Console.WriteLine("Presione (Y) para agregar a una persona, (N) para ir atras");
                            char des = Console.ReadLine()[0];
                            switch (char.ToLower(des))
                            {
                                case 'y':
                                    Console.Write("Escriba el nombre de la persona: ");
                                    string nombre = Console.ReadLine();
                                    SQLManager.EnviarInvitacion("perfiles_registrados", nombre, this.currentPerfil);
                                    Console.ReadLine();
                                    break;
                                case 'n':
                                    break;
                            }
                            Console.ReadLine();
                            break;
                        case 3:
                            Console.Clear();
                            Console.WriteLine("1. Crear un chat privado");
                            Console.WriteLine("2. Ver chats");
                            Console.WriteLine("3. Volver");
                            byte opcion;

                            if(byte.TryParse(Console.ReadLine(),out opcion))
                            {
                                switch (opcion)
                                {
                                    case 1:
                                        appState.ChangeState(new CreatingChatState(currentPerfil));
                                        break;
                                    case 2:
                                        
                                            Console.Clear();
                                            Console.WriteLine("Chats Existentes");
                                            Console.WriteLine();
                                            SQLManager.VerChats(currentPerfil);
                                            Console.WriteLine();
                                            Console.WriteLine("1. Volver");
                                            Console.WriteLine("Escriba el nombre del Chat al que desea ingresar");
                                            string perfil = Console.ReadLine();

                                            if (SQLManager.ValidarExistenciaAmigo(this.currentPerfil.Nombre, perfil) == true)
                                            {
                                                Perfil perfil2 = new Perfil(perfil, "", "", 0, 0);
                                                Chat chat = new Chat(currentPerfil, perfil2);
                                                appState.ChangeState(new ChattingState(chat, perfil));
                                                break;
                                            }
                                            else if(perfil == "1")
                                            {
                                                break;
                                            }         
                                            else
                                            {
                                                Console.WriteLine("El Usuario no esta en su lista de amigo");
                                                Console.ReadKey();
                                                continue;
                                            }
                                                                         
                                    case 3:
                                        break;
                                    default:
                                        continue;
                                }
                            }                           
                            break;
                        case 4:
                            {
                                Console.Clear();
                                SQLManager.ImprimirTodosCirculos();
                                Console.Write("\nEscriba el nombre del circulo al que desea entrar: ");
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
                                    string query = "DELETE FROM circulos WHERE nombre = " + "'" + nombreCirculo + "'";
                                    string query2 = "DELETE FROM posts WHERE circulo_pertenece = " + "'" + nombreCirculo + "'";
                                    string query3 = "DELETE FROM comentarios WHERE circulo_pertenece =" + "'" + nombreCirculo + "'";
                                    string query4 = "DELETE FROM comentarios_comentarios WHERE circulo_pertenece = " + "'" + nombreCirculo + "'";
                                    SQLManager.EjecutarQuery(query, query2, query3, query4);
                                    Console.WriteLine("El círculo fue borrado.");
                                }
                                else
                                    Console.WriteLine("Ese círculo no existe.");
                                Console.ReadLine();
                                break;
                            }
                        case 7:
                            {
                                Console.Clear();
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
                        default:
                            continue;
                    }
                }
            }
        }


        public void Inicializar(Perfil perfil)
        {
             this.currentPerfil = perfil;
             instance = this;
        }
    }
}
