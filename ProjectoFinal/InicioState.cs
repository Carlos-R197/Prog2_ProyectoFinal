using System;
using System.Collections.Generic;
using System.Text;
using MySql.Data.MySqlClient;
using ProjectoFinal.Data;

namespace ProjectoFinal
{
    public class InicioState : IState
    {
        public void Handle(StateMachine appState)
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine("¿Qué desea hacer?");
                Console.WriteLine("1. Ingresar");
                Console.WriteLine("2. Registrarse");
                Console.WriteLine("3. Salir de la aplicación");
                Console.Write("R: ");
                byte input;

                if (byte.TryParse(Console.ReadLine(), out input))
                {
                    switch (input)
                    {
                        case 1:
                            appState.ChangeState(new SignInState());
                            break;
                        case 2:
                            appState.ChangeState(new SignUpState());
                            break;
                        case 3:
                            System.Environment.Exit(0);
                            break;
                        default:
                            continue;
                    }
                }
            }
        }
    }
}
