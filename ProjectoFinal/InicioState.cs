using System;
using System.Collections.Generic;
using System.Text;

namespace ProjectoFinal
{
    public class InicioState : IState
    {
        private StateMachine appState;
        public InicioState(StateMachine machine)
        {
            this.appState = machine;
        }

        public void Enter()
        {
            while (true)
            {
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
                            break;
                        case 2:
                            break;
                        case 3:
                            return;
                        default:
                            continue;
                    }
                }
            }
        }
    }
}
