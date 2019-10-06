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
                //Cambio en el perfil
                Console.WriteLine("UPDATE 'perfiles_registrados' SET 'nombre' = \"" + "a" + "\", 'contasena' = \"" + "b" + "\", 'correo' = \"" + "c" + "\", 'edad' = \"" + "d" + "\" WHERE 'nombre' = \"" + "e" + "\"");
                //Cambio en el perfil
                Console.WriteLine("¿Qué desea hacer?");
                Console.WriteLine("1. Ingresar");
                Console.WriteLine("2. Registrarse");
                Console.WriteLine("3. Salir de la aplicación");
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
