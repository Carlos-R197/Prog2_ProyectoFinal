using System;
using System.Collections.Generic;
using System.Text;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class MainMenuState : IState
    {
        private Perfil currentPerfil;
        private StateMachine appState;
        public MainMenuState(StateMachine machine, Perfil perfil)
        {
            this.appState = machine;
            currentPerfil = perfil;
        }

        public void Enter()
        {
            while (true)
            {
                Console.Clear();

                Console.WriteLine("Nombre: {0}", currentPerfil.Nombre);
                Console.WriteLine("Rating General: {0} \n", currentPerfil.RatingGeneral);

                Console.WriteLine("¿Qué desea hacer?");
                Console.WriteLine("1. Ver propio perfil");
                Console.WriteLine("2. Buscar un perfil existente");
                Console.WriteLine("3. Crear un chat privado");
                Console.WriteLine("4. Ver Lista de circulos existente");
                Console.WriteLine("5. Salir de la aplicación");
                Console.Write("R: ");
                byte input;

                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            currentPerfil.ImprimirInformacion();
                            break;
                        case 2:
                            break;
                        case 3:
                            break;
                        case 4:
                            Environment.Exit(0);
                            break;
                        case 5:
                            
                        default:
                            continue;
                    }
                }
            }
        }
    }
}
